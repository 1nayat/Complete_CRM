using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AskKhadim.HRMS.Domain.Employees;
using AskKhadim.HRMS.Domain.Repository;

namespace AskKhadim.HRMS.Application.Employees.Update;

public sealed class UpdateEmployeeHandler
{
    private readonly IEmployeeRepository _repo;

    public UpdateEmployeeHandler(IEmployeeRepository repo)
    {
        _repo = repo;
    }

    public async Task Handle(UpdateEmployeeCommand cmd)
    {
        var emp = new Employee(
            employeeCode: string.Empty, // not changed
            email: string.Empty,
            firstName: cmd.FirstName,
            lastName: cmd.LastName,
            departmentId: cmd.DepartmentId,
            designation: cmd.Designation
        );

        await _repo.UpdateAsync(cmd.UserId, emp);
    }
}

