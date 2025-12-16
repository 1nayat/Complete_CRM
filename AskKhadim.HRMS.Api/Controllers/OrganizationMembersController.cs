using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using AskKhadim.HRMS.Infrastructure.Data;
using AskKhadim.HRMS.Infrastructure.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Authorize(Roles = "ClientAdmin")]
[ApiController]
[Route("api/organization/members")]
public class OrganizationMembersController : ControllerBase
{
    private readonly AskKhadimDbContext _db;
    private readonly IEmailService _emailService;

    public OrganizationMembersController(
        AskKhadimDbContext db,
        IEmailService emailService)
    {
        _db = db;
        _emailService = emailService;
    }

    public class InviteMemberDto
    {
        public string Email { get; set; } = null!;
        public Guid RoleId { get; set; }
        public string? Designation { get; set; }
    }

    [HttpPost("invite")]
    public async Task<IActionResult> Invite([FromBody] InviteMemberDto dto)
    {
        var adminId = long.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        var admin = await _db.core_users
            .Include(u => u.organization)
            .FirstAsync(u => u.id == adminId);

        if (admin.organization_id == null)
            return BadRequest("Admin has no organization.");

        var orgContactEmail = await _db.organization_contacts
            .Where(c =>
                c.organization_id == admin.organization_id &&
                c.is_primary)
            .Select(c => c.email)
            .FirstOrDefaultAsync();

        var rawToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
        var tokenHash = SHA256.HashData(Encoding.UTF8.GetBytes(rawToken));

        var invite = new organization_invitation
        {
            invitation_id = Guid.NewGuid(),
            organization_id = admin.organization_id.Value,
            email = dto.Email.ToLower(),
            role_id = dto.RoleId,
            designation = dto.Designation,
            invite_token_hash = tokenHash,
            expires_at = DateTime.UtcNow.AddDays(3),
            invited_by = adminId
        };

        _db.organization_invitations.Add(invite);
        await _db.SaveChangesAsync();

        var inviteLink =
            $"https://app.yourcrm.com/accept-invite?token={Uri.EscapeDataString(rawToken)}";

        await _emailService.SendAsync(
            to: dto.Email,
            subject: $"Invitation to join {admin.organization!.name}",
            body: $@"
                <p>Hello,</p>

                <p>
                    You have been invited to join
                    <b>{admin.organization.name}</b>
                    as <b>{dto.Designation ?? "Member"}</b>.
                </p>

                <p>
                    <a href='{inviteLink}'>Click here to set your password</a>
                </p>

                <p>This link expires in 3 days.</p>
            ",
            replyTo: orgContactEmail ?? admin.email 
        );

        return Ok(new
        {
            message = "Invitation sent",
            inviteToken = rawToken   
        });

    }
}
