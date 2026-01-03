using AskKhadim.HRMS.Domain.Employees;
using AskKhadim.HRMS.Domain.Repository;
using AskKhadim.HRMS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AskKhadim.HRMS.Infrastructure.Repositories;

public sealed class EmployeeReadRepository : IEmployeeReadRepository
{
    private readonly AskKhadimDbContext _db;

    public EmployeeReadRepository(AskKhadimDbContext db)
    {
        _db = db;
    }

    public async Task<EmployeeProfileDto?> GetByIdAsync(long userId)
    {
        return await (
            from u in _db.core_users
            join up in _db.user_profiles on u.id equals up.user_id
            join uh in _db.user_hrs on u.id equals uh.user_id
            join d in _db.departments on uh.department_id equals d.department_id
            where u.id == userId
            select new EmployeeProfileDto
            {
                UserId = u.id,
                EmployeeCode = u.employee_id,
                Email = u.email,
                FullName = up.first_name + " " + up.last_name,
                Department = d.department_name,
                Designation = uh.designation,
                EmploymentStatus = uh.employment_status,
                IsActive = u.is_active
            }
        ).FirstOrDefaultAsync();
    }

    public async Task<List<EmployeeProfileDto>> GetAllAsync()
    {
        return await (
            from u in _db.core_users
            join up in _db.user_profiles on u.id equals up.user_id
            join uh in _db.user_hrs on u.id equals uh.user_id
            join d in _db.departments on uh.department_id equals d.department_id
            select new EmployeeProfileDto
            {
                UserId = u.id,
                EmployeeCode = u.employee_id,
                Email = u.email,
                FullName = up.first_name + " " + up.last_name,
                Department = d.department_name,
                Designation = uh.designation,
                EmploymentStatus = uh.employment_status,
                IsActive = u.is_active
            }
        ).ToListAsync();
    }
}
