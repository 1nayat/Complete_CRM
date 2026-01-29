using System.Security;
using AskKhadim.HRMS.Application.Common.Security;
using AskKhadim.HRMS.Domain.Employees;
using AskKhadim.HRMS.Domain.Repository;
using AskKhadim.HRMS.Infrastructure.Data;
using AskKhadim.HRMS.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace AskKhadim.HRMS.Infrastructure.Repositories;

public sealed class EmployeeRepository : IEmployeeRepository
{
    private readonly AskKhadimDbContext _db;
    private readonly ICurrentUser _currentUser;

    public EmployeeRepository(
        AskKhadimDbContext db,
        ICurrentUser currentUser)
    {
        _db = db;
        _currentUser = currentUser;
    }

    public async Task<long> CreateAsync(Employee emp, DateTime joiningDate)
    {
        using var tx = await _db.Database.BeginTransactionAsync();

        var departmentExists = await _db.departments
            .AnyAsync(d => d.department_id == emp.DepartmentId);

        if (!departmentExists)
            throw new InvalidOperationException("Invalid department for this organization.");

        var user = new core_user
        {
            user_uuid = Guid.NewGuid(),
            employee_id = emp.EmployeeCode,
            email = emp.Email,
            password_hash = "TEMP_HASH",
            is_active = false, // 🔒 cannot login until invite accepted
            organization_id = _currentUser.OrganizationId
                ?? throw new SecurityException("Organization context missing")
        };

        _db.core_users.Add(user);
        await _db.SaveChangesAsync();

        _db.user_profiles.Add(new user_profile
        {
            user_id = user.id,
            first_name = emp.FirstName,
            last_name = emp.LastName
        });

        // 4️⃣ HR record (ONBOARDING, not Active)
        _db.user_hrs.Add(new user_hr
        {
            user_id = user.id,
            department_id = emp.DepartmentId,
            designation = emp.Designation,
            joining_date = DateOnly.FromDateTime(joiningDate),
            employment_status = "Onboarding"
        });

        // 5️⃣ Audit log
        _db.audit_logs.Add(new audit_log
        {
            actor_user_id = _currentUser.UserId,
            actor_role = "ClientAdmin",
            action_type = "EMPLOYEE_CREATED",
            entity_type = "core_user",
            entity_id = user.id.ToString(),
            timestamp = DateTime.UtcNow
        });

        await _db.SaveChangesAsync();
        await tx.CommitAsync();

        return user.id;
    }


    public async Task UpdateAsync(long userId, Employee emp)
    {
        var hr = await _db.user_hrs.FirstAsync(x => x.user_id == userId);
        var profile = await _db.user_profiles.FirstAsync(x => x.user_id == userId);

        _db.user_employment_histories.Add(new user_employment_history
        {
            user_id = userId,
            department_id = hr.department_id,
            designation = hr.designation,
            effective_from = DateOnly.FromDateTime(DateTime.UtcNow),
            changed_by = _currentUser.UserId
        });

        hr.department_id = emp.DepartmentId;
        hr.designation = emp.Designation;
        profile.first_name = emp.FirstName;
        profile.last_name = emp.LastName;

        await _db.SaveChangesAsync();
    }

    public async Task DeactivateAsync(long userId)
    {
        var user = await _db.core_users.FirstAsync(x => x.id == userId);
        var hr = await _db.user_hrs.FirstAsync(x => x.user_id == userId);

        user.is_active = false;
        hr.employment_status = "Terminated";
        hr.exit_date = DateOnly.FromDateTime(DateTime.UtcNow);

        await _db.SaveChangesAsync();
    }
}
