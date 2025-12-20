namespace AskKhadim.HRMS.Application.Common.Security;
public interface ICurrentUser
{
    long UserId { get; }
    Guid? OrganizationId { get; }
    string Role { get; }
    bool IsAuthenticated { get; }
    bool IsSuperAdmin { get; }
}
