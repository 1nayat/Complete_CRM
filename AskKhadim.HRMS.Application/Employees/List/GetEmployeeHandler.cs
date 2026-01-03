using AskKhadim.HRMS.Domain.Employees;
using AskKhadim.HRMS.Domain.Repository;

namespace AskKhadim.HRMS.Application.Employees.List;

public sealed class GetEmployeesHandler
{
    private readonly IEmployeeReadRepository _repo;

    public GetEmployeesHandler(IEmployeeReadRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<EmployeeProfileDto>> Handle()
    {
        return await _repo.GetAllAsync();
    }
}
