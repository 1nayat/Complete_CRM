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

        // 1️⃣ Find existing employee user (MUST exist)
        var user = await _db.core_users
            .FirstOrDefaultAsync(u =>
                u.email == invite.email &&
                u.organization_id == invite.organization_id);

        if (user == null)
            return BadRequest("Employee record not found. Please contact admin.");

        // 2️⃣ Update password + activate account
        user.password_hash = BCrypt.Net.BCrypt.HashPassword(dto.Password);
        user.is_active = true;
        user.email_verified = true;
        user.updated_at = DateTime.UtcNow;

        // 3️⃣ Transition HR status: Onboarding → Active
        var userHr = await _db.user_hrs
            .FirstOrDefaultAsync(h => h.user_id == user.id);

        if (userHr == null)
            return BadRequest("HR record not found for employee.");

        userHr.employment_status = "Active";
        userHr.confirmation_date = DateOnly.FromDateTime(DateTime.UtcNow);
        userHr.updated_at = DateTime.UtcNow;

        // 4️⃣ Assign role
        _db.user_roles.Add(new user_role
        {
            user_id = user.id,
            role_id = invite.role_id,
            organization_id = invite.organization_id,
            assigned_at = DateTime.UtcNow
        });

        // 5️⃣ Mark invite as accepted (keep for audit)
        invite.accepted_at = DateTime.UtcNow;

        await _db.SaveChangesAsync();

        return Ok("Invitation accepted. Account activated.");
    }
}
