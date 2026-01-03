using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AskKhadim.HRMS.Domain.Repository;

namespace AskKhadim.HRMS.Application.Employees.Deactivate;

public sealed class DeactivateEmployeeHandler
{
    private readonly IEmployeeRepository _repo;

    public DeactivateEmployeeHandler(IEmployeeRepository repo)
    {
        _repo = repo;
    }

    public async Task Handle(DeactivateEmployeeCommand cmd)
    {
        await _repo.DeactivateAsync(cmd.UserId);
    }
}
