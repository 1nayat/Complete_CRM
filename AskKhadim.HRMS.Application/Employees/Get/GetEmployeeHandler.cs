using AskKhadim.HRMS.Domain.Employees;
using AskKhadim.HRMS.Domain.Repository;

namespace AskKhadim.HRMS.Application.Employees.Get;

public sealed class GetEmployeeHandler
{
    private readonly IEmployeeReadRepository _repo;

    public GetEmployeeHandler(IEmployeeReadRepository repo)
    {
        _repo = repo;
    }

    public async Task<EmployeeProfileDto?> Handle(long userId)
    {
        return await _repo.GetByIdAsync(userId);
    }
}
