using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AskKhadim.HRMS.Domain.Employees;
using AskKhadim.HRMS.Domain.Repository;

namespace AskKhadim.HRMS.Application.Employees.Create;

public sealed class CreateEmployeeHandler
{
    private readonly IEmployeeRepository _repository;

    public CreateEmployeeHandler(IEmployeeRepository repository)
    {
        _repository = repository;
    }

    public Task<long> Handle(CreateEmployeeCommand command)
    {
        var employee = new Employee(
            command.EmployeeCode,
            command.Email,
            command.FirstName,
            command.LastName,
            command.DepartmentId,
            command.Designation
        );

        return _repository.CreateAsync(employee, command.JoiningDate);
    }
}
