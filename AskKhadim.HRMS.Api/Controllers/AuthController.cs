using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using AskKhadim.HRMS.Infrastructure.Data;
using AskKhadim.HRMS.Infrastructure.Models;
using System.IdentityModel.Tokens.Jwt;

namespace AskKhadim.HRMS.Api.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly AskKhadimDbContext _db;
        private readonly ITokenService _tokenService;
        private readonly IRefreshTokenRepository _rtRepo;
        private readonly IConfiguration _cfg;

        public AuthController(
            AskKhadimDbContext db,
            ITokenService tokenService,
            IRefreshTokenRepository rtRepo,
            IConfiguration cfg)
        {
            _db = db;
            _tokenService = tokenService;
            _rtRepo = rtRepo;
            _cfg = cfg;
        }


        public class LoginDto
        {
            public string Email { get; set; } = null!;
            public string Password { get; set; } = null!;
        }

        public class RefreshDto
        {
            public string RefreshToken { get; set; } = null!;
        }


        private static byte[] HashToken(string token)
        {
            using var sha = SHA256.Create();
            return sha.ComputeHash(Encoding.UTF8.GetBytes(token));
        }

        private async Task<List<string>> GetUserRoles(long userId)
        {
            return await (
                from ur in _db.user_roles
                join r in _db.roles on ur.role_id equals r.role_id
                where ur.user_id == userId
                select r.role_name
            ).ToListAsync();
        }

        private List<Claim> BuildClaims(core_user user, List<string> roles)
        {
            var claims = new List<Claim>
    {
        new Claim(JwtRegisteredClaimNames.Sub, user.id.ToString()),

        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),

        new Claim(JwtRegisteredClaimNames.Email, user.email),

        new Claim("token_type", "access"),
        new Claim("ver", "1")
    };

            if (user.organization_id.HasValue)
            {
                claims.Add(new Claim("org_id", user.organization_id.Value.ToString()));
            }

            claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));

            return claims;
        }



        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var email = dto.Email.Trim().ToLowerInvariant();

            var user = await _db.core_users.FirstOrDefaultAsync(u => u.email == email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.password_hash))
                return Unauthorized("Invalid credentials");
            user.last_login = DateTime.UtcNow;
            user.updated_at = DateTime.UtcNow;
            await _db.SaveChangesAsync();
            if (!user.is_active)
                return Forbid("Account disabled");

            var roles = await GetUserRoles(user.id);
            var claims = BuildClaims(user, roles);

            var accessToken = _tokenService.GenerateAccessToken(claims);
            var refreshToken = _tokenService.GenerateRefreshToken();
            var refreshHash = HashToken(refreshToken);

            int refreshDays = int.Parse(_cfg["Jwt:RefreshTokenDays"] ?? "30");
            var ip = HttpContext.Connection.RemoteIpAddress?.ToString();

            await _rtRepo.AddAsync(new refresh_token
            {
                user_id = user.id,
                token_hash = refreshHash,
                created_at = DateTime.UtcNow,
                expires_at = DateTime.UtcNow.AddDays(refreshDays),
                revoked = false,
                created_by_ip = ip
            });

            return Ok(new
            {
                accessToken,
                refreshToken
            });
        }


        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshDto dto)
        {
            var tokenHash = HashToken(dto.RefreshToken);

            var storedToken = await _rtRepo.GetActiveByHashAsync(tokenHash);
            if (storedToken == null)
                return Unauthorized("Invalid refresh token");

            var user = await _db.core_users.FindAsync(storedToken.user_id);
            if (user == null || !user.is_active)
                return Unauthorized();

            storedToken.revoked = true;
            storedToken.revoked_at = DateTime.UtcNow;
            storedToken.revoked_reason = "Rotated";
            await _rtRepo.UpdateAsync(storedToken);

            var roles = await GetUserRoles(user.id);
            var claims = BuildClaims(user, roles);

            var newAccessToken = _tokenService.GenerateAccessToken(claims);
            var newRefreshToken = _tokenService.GenerateRefreshToken();
            var newRefreshHash = HashToken(newRefreshToken);

            int refreshDays = int.Parse(_cfg["Jwt:RefreshTokenDays"] ?? "30");
            var ip = HttpContext.Connection.RemoteIpAddress?.ToString();

            await _rtRepo.AddAsync(new refresh_token
            {
                user_id = user.id,
                token_hash = newRefreshHash,
                replaced_by_token_hash = tokenHash,
                created_at = DateTime.UtcNow,
                expires_at = DateTime.UtcNow.AddDays(refreshDays),
                revoked = false,
                created_by_ip = ip
            });

            return Ok(new
            {
                accessToken = newAccessToken,
                refreshToken = newRefreshToken
            });
        }


        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromBody] RefreshDto dto)
        {
            var tokenHash = HashToken(dto.RefreshToken);

            var storedToken = await _rtRepo.GetActiveByHashAsync(tokenHash);
            if (storedToken != null)
            {
                storedToken.revoked = true;
                storedToken.revoked_at = DateTime.UtcNow;
                storedToken.revoked_reason = "Logout";
                await _rtRepo.UpdateAsync(storedToken);
            }

            return Ok();
        }
    }
}
