using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AskKhadim.HRMS.Infrastructure.Data;
using AskKhadim.HRMS.Infrastructure.Models;
using AskKhadim.HRMS.Api.Dtos;

[ApiController]
[Route("api/invites")]
public class OrganizationInvitesController : ControllerBase
{
    private readonly AskKhadimDbContext _db;

    public OrganizationInvitesController(AskKhadimDbContext db)
    {
        _db = db;
    }
  


    [HttpPost("accept")]
    public async Task<IActionResult> AcceptInvite([FromBody] AcceptInviteDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Token) || string.IsNullOrWhiteSpace(dto.Password))
            return BadRequest("Token and password are required.");

        var tokenHash = SHA256.HashData(Encoding.UTF8.GetBytes(dto.Token));

        var invite = await _db.organization_invitations
            .FirstOrDefaultAsync(i =>
                i.invite_token_hash == tokenHash &&
                i.expires_at > DateTime.UtcNow);

        if (invite == null)
            return BadRequest("Invalid or expired invitation.");

        var existingUser = await _db.core_users
            .FirstOrDefaultAsync(u => u.email == invite.email);

        core_user user;

        if (existingUser == null)
        {
            user = new core_user
            {
                user_uuid = Guid.NewGuid(),
                email = invite.email,
                employee_id = invite.email,
                password_hash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                is_active = true,
                email_verified = true,
                organization_id = invite.organization_id,
                created_at = DateTime.UtcNow,
                updated_at = DateTime.UtcNow
            };

            _db.core_users.Add(user);
            await _db.SaveChangesAsync();
        }
        else
        {
            return Conflict("User already exists. Please login.");
        }

        var userRole = new user_role
        {
            user_id = user.id,
            role_id = invite.role_id,
            organization_id = invite.organization_id,
            assigned_at = DateTime.UtcNow
        };

        _db.user_roles.Add(userRole);

        _db.organization_invitations.Remove(invite);

        await _db.SaveChangesAsync();

        return Ok("Invitation accepted. Account created.");
    }
}
