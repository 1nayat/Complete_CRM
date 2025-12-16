using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AskKhadim.HRMS.Infrastructure.Data;
using AskKhadim.HRMS.Infrastructure.Models;

namespace AskKhadim.HRMS.Api.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class AccountController : ControllerBase
    {
        private readonly AskKhadimDbContext _db;
        private readonly ITokenService _tokenService;
        private readonly IRefreshTokenRepository _rtRepo;
        private readonly IConfiguration _cfg;

        public AccountController(
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

     

        private static byte[] HashToken(string token)
        {
            using var sha = SHA256.Create();
            return sha.ComputeHash(Encoding.UTF8.GetBytes(token));
        }

      

        public class RegisterDto
        {
            public string FullName { get; set; } = null!;
            public string Email { get; set; } = null!;
            public string Password { get; set; } = null!;
            public string? EmployeeId { get; set; }
            public string? AdminUsername { get; set; }
        }

        public class RegisterResult
        {
            public long UserId { get; set; }
            public string AccessToken { get; set; } = null!;
            public string RefreshToken { get; set; } = null!;
            public string RedirectTo { get; set; } = "/forms";
        }

        

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            if (dto == null ||
                string.IsNullOrWhiteSpace(dto.Email) ||
                string.IsNullOrWhiteSpace(dto.Password))
                return BadRequest("Email and password are required.");

            var email = dto.Email.Trim().ToLowerInvariant();

            if (await _db.core_users.AnyAsync(u => u.email == email))
                return Conflict("Email already registered.");

            using var tx = await _db.Database.BeginTransactionAsync();

            try
            {
                var user = new core_user
                {
                    user_uuid = Guid.NewGuid(),
                    employee_id = string.IsNullOrWhiteSpace(dto.EmployeeId)
                        ? (dto.AdminUsername ?? email)
                        : dto.EmployeeId,
                    email = email,
                    password_hash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                    is_active = true,
                    email_verified = false,
                    created_at = DateTime.UtcNow,
                    updated_at = DateTime.UtcNow
                };

                _db.core_users.Add(user);
                await _db.SaveChangesAsync();

                var role = await _db.roles
                    .FirstOrDefaultAsync(r => r.role_name == "ClientAdmin");

                if (role == null)
                {
                    role = new role
                    {
                        role_id = Guid.NewGuid(),
                        role_name = "ClientAdmin",
                        description = "Client-level administrator",
                        created_at = DateTime.UtcNow
                    };
                    _db.roles.Add(role);
                    await _db.SaveChangesAsync();
                }

                _db.user_roles.Add(new user_role
                {
                    user_id = user.id,
                    role_id = role.role_id,
                    organization_id = null,
                    assigned_at = DateTime.UtcNow
                });

                await _db.SaveChangesAsync();

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.id.ToString()),
                    new Claim(ClaimTypes.Email, user.email),
                    new Claim(ClaimTypes.Role, "ClientAdmin")
                };

                var accessToken = _tokenService.GenerateAccessToken(claims);
                var refreshToken = _tokenService.GenerateRefreshToken();
                var refreshHash = HashToken(refreshToken);

                int refreshDays = int.Parse(_cfg["Jwt:RefreshTokenDays"] ?? "30");

                await _rtRepo.AddAsync(new refresh_token
                {
                    user_id = user.id,
                    token_hash = refreshHash,
                    created_at = DateTime.UtcNow,
                    expires_at = DateTime.UtcNow.AddDays(refreshDays),
                    revoked = false,
                    created_by_ip = HttpContext.Connection.RemoteIpAddress?.ToString()
                });

                await tx.CommitAsync();

                return Created("", new RegisterResult
                {
                    UserId = user.id,
                    AccessToken = accessToken,
                    RefreshToken = refreshToken,
                    RedirectTo = "/forms"
                });
            }
            catch
            {
                await tx.RollbackAsync();
                throw;

            }
        }
    }
}
