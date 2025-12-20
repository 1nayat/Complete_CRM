using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using AskKhadim.HRMS.Application.Common.Security;

namespace AskKhadim.HRMS.Api.Security;

public sealed class CurrentUser : ICurrentUser
{
    public long UserId { get; private set; }
    public Guid? OrganizationId { get; private set; }
    public string Role { get; private set; } = string.Empty;
    public bool IsAuthenticated { get; private set; }
    public bool IsSuperAdmin => Role == "SuperAdmin";

    public CurrentUser(IHttpContextAccessor accessor)
    {
        var user = accessor.HttpContext?.User;

        if (user?.Identity?.IsAuthenticated != true)
        {
            IsAuthenticated = false;
            return;
        }

        IsAuthenticated = true;

        var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim != null && long.TryParse(userIdClaim.Value, out var uid))
            UserId = uid;

        Role = user.FindFirstValue(ClaimTypes.Role) ?? string.Empty;

        var orgClaim = user.FindFirst("org_id");
        if (orgClaim != null && Guid.TryParse(orgClaim.Value, out var orgId))
            OrganizationId = orgId;
    }
}
