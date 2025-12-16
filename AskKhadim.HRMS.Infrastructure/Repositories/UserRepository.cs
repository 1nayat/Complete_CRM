using AskKhadim.HRMS.Infrastructure.Data;
using AskKhadim.HRMS.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

public interface IUserRepository
{
    Task<core_user> GetByEmailAsync(string email);
    Task<core_user> GetByIdAsync(long id);
    Task<IList<string>> GetRolesAsync(long userId);
}

public class UserRepository : IUserRepository
{
    private readonly AskKhadimDbContext _db;
    public UserRepository(AskKhadimDbContext db) => _db = db;

    public async Task<core_user> GetByEmailAsync(string email)
        => await _db.core_users.FirstOrDefaultAsync(u => u.email == email);

    public async Task<core_user> GetByIdAsync(long id)
        => await _db.core_users.FindAsync(id);

    public async Task<IList<string>> GetRolesAsync(long userId)
    {
        // Adjust column/property names if scaffold differs
        var q = from ur in _db.user_roles
                join r in _db.roles on ur.role_id equals r.role_id
                where ur.user_id == userId
                select r.role_name; // or r.name

        return await q.ToListAsync();
    }
}
