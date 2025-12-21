using AskKhadim.HRMS.Application.Employees.Create;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AskKhadim.HRMS.Api.Controllers;

[Authorize(Roles = "ClientAdmin,SuperAdmin")]
[ApiController]
[Route("api/employees")]
public sealed class EmployeesController : ControllerBase
{
    private readonly CreateEmployeeHandler _handler;

    public EmployeesController(CreateEmployeeHandler handler)
    {
        _handler = handler;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateEmployeeCommand command)
    {
        var id = await _handler.Handle(command);
        return CreatedAtAction(nameof(Create), new { id }, null);
    }
}
