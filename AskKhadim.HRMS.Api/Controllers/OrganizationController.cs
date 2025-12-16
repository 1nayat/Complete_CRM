// Api/Controllers/OrganizationController.cs
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AskKhadim.HRMS.Infrastructure.Data;
using AskKhadim.HRMS.Infrastructure.Models;

namespace AskKhadim.HRMS.Api.Controllers
{
    [ApiController]
    [Route("api/organization")]
    [Authorize(Roles = "ClientAdmin")]
    public class OrganizationController : ControllerBase
    {
        private readonly AskKhadimDbContext _db;

        public OrganizationController(AskKhadimDbContext db)
        {
            _db = db;
        }

        #region DTO

        public class OrganizationRegisterDto
        {
            // Organization
            public string Name { get; set; } = null!;
            public string? OrganizationType { get; set; }
            public string? Industry { get; set; }
            public string? TaxRegistrationNumber { get; set; }
            public int? YearEstablished { get; set; }
            public string? CompanySize { get; set; }
            public string? WebsiteUrl { get; set; }
            public string? BriefDescription { get; set; }
            public string? PrimaryProducts { get; set; }
            public string? TargetMarket { get; set; }
            public string? RevenueRange { get; set; }
            public string? PreferredPlan { get; set; }
            public int? ExpectedUserCount { get; set; }
            public string? PreferredLanguage { get; set; }
            public string? TimeZone { get; set; }

            // Primary contact
            public string ContactFullName { get; set; } = null!;
            public string? ContactJobTitle { get; set; }
            public string ContactEmail { get; set; } = null!;
            public string ContactPhone { get; set; } = null!;
            public string? ContactAltPhone { get; set; }

            // Address
            public string? AddressLine1 { get; set; }
            public string? AddressLine2 { get; set; }
            public string? City { get; set; }
            public string? StateProvince { get; set; }
            public string? PostalCode { get; set; }
            public string? Country { get; set; }
        }

        #endregion

      
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] OrganizationRegisterDto dto)
        {
            if (dto == null) return BadRequest("Payload required.");
            if (string.IsNullOrWhiteSpace(dto.Name)) return BadRequest("Organization name is required.");
            if (string.IsNullOrWhiteSpace(dto.ContactFullName) || string.IsNullOrWhiteSpace(dto.ContactEmail))
                return BadRequest("Primary contact name and email are required.");

            var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!long.TryParse(userIdStr, out var userId))
                return Unauthorized("Invalid user.");

            await using var tx = await _db.Database.BeginTransactionAsync();
            try
            {
                var org = new organization
                {
                    organization_id = Guid.NewGuid(),
                    name = dto.Name.Trim(),
                    organization_type = dto.OrganizationType,
                    industry = dto.Industry,
                    tax_registration_number = dto.TaxRegistrationNumber,
                    year_established = dto.YearEstablished,
                    company_size = dto.CompanySize,
                    website_url = dto.WebsiteUrl,
                    brief_description = dto.BriefDescription,
                    primary_products = dto.PrimaryProducts,
                    target_market = dto.TargetMarket,
                    revenue_range = dto.RevenueRange,
                    preferred_plan = dto.PreferredPlan,
                    expected_user_count = dto.ExpectedUserCount,
                    preferred_language = dto.PreferredLanguage,
                    time_zone = dto.TimeZone,
                    is_active = true,
                    created_at = DateTime.UtcNow,
                    created_by = userId
                };

                _db.organizations.Add(org);
                await _db.SaveChangesAsync();

                var user = await _db.core_users.FirstOrDefaultAsync(u => u.id == userId);
                if (user == null)
                    return Unauthorized();

                user.organization_id = org.organization_id;
                user.updated_at = DateTime.UtcNow;

                _db.core_users.Update(user);
                await _db.SaveChangesAsync();

                var contact = new organization_contact
                {
                    organization_id = org.organization_id,
                    full_name = dto.ContactFullName,
                    job_title = dto.ContactJobTitle,
                    email = dto.ContactEmail,
                    phone = dto.ContactPhone,
                    alt_phone = dto.ContactAltPhone,
                    is_primary = true,
                    created_at = DateTime.UtcNow,
                    created_by = userId
                };

                _db.organization_contacts.Add(contact);
                await _db.SaveChangesAsync();

                var hasAddress =
                    !string.IsNullOrWhiteSpace(dto.AddressLine1) ||
                    !string.IsNullOrWhiteSpace(dto.City) ||
                    !string.IsNullOrWhiteSpace(dto.Country);

                if (hasAddress)
                {
                    var address = new organization_address
                    {
                        organization_id = org.organization_id,
                        address_line1 = dto.AddressLine1,
                        address_line2 = dto.AddressLine2,
                        city = dto.City,
                        state_province = dto.StateProvince,
                        postal_code = dto.PostalCode,
                        country = dto.Country,
                        created_at = DateTime.UtcNow
                    };

                    _db.organization_addresses.Add(address);
                    await _db.SaveChangesAsync();
                }

                var rolesForUser = await _db.user_roles
                    .Where(ur => ur.user_id == userId &&
                                 (ur.organization_id == null || ur.organization_id == Guid.Empty))
                    .ToListAsync();

                if (rolesForUser.Any())
                {
                    foreach (var ur in rolesForUser)
                        ur.organization_id = org.organization_id;

                    _db.user_roles.UpdateRange(rolesForUser);
                    await _db.SaveChangesAsync();
                }
                else
                {
                    var clientAdminRole = await _db.roles
                        .FirstOrDefaultAsync(r => r.role_name == "ClientAdmin");

                    if (clientAdminRole != null)
                    {
                        _db.user_roles.Add(new user_role
                        {
                            user_id = userId,
                            role_id = clientAdminRole.role_id,
                            organization_id = org.organization_id,
                            assigned_at = DateTime.UtcNow
                        });
                        await _db.SaveChangesAsync();
                    }
                }

                await tx.CommitAsync();

                return CreatedAtAction(nameof(Register), new
                {
                    organizationId = org.organization_id,
                    message = "Organization created and permanently linked to the user."
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
