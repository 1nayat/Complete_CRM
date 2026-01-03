using AskKhadim.HRMS.Application.Employees.Create;
using AskKhadim.HRMS.Application.Employees.Get;
using AskKhadim.HRMS.Application.Employees.Update;
using AskKhadim.HRMS.Application.Employees.Deactivate;
using AskKhadim.HRMS.Application.Employees.List;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AskKhadim.HRMS.Api.Controllers;

[Authorize(Roles = "ClientAdmin,SuperAdmin")]
[ApiController]
[Route("api/employees")]
public sealed class EmployeesController : ControllerBase
{
    private readonly CreateEmployeeHandler _create;
    private readonly GetEmployeeHandler _get;
    private readonly UpdateEmployeeHandler _update;
    private readonly DeactivateEmployeeHandler _deactivate;
    private readonly GetEmployeesHandler _list;

    public EmployeesController(
        CreateEmployeeHandler create,
        GetEmployeeHandler get,
        UpdateEmployeeHandler update,
        DeactivateEmployeeHandler deactivate,
        GetEmployeesHandler list)
    {
        _create = create;
        _get = get;
        _update = update;
        _deactivate = deactivate;
        _list = list;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateEmployeeCommand cmd)
    {
        var id = await _create.Handle(cmd);
        return CreatedAtAction(nameof(Get), new { id }, null);
    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult> Get(long id)
    {
        var result = await _get.Handle(id);
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPut("{id:long}")]
    public async Task<IActionResult> Update(long id, UpdateEmployeeCommand cmd)
    {
        await _update.Handle(cmd with { UserId = id });
        return NoContent();
    }

    [HttpPatch("{id:long}/deactivate")]
    public async Task<IActionResult> Deactivate(long id)
    {
        await _deactivate.Handle(new DeactivateEmployeeCommand(id));
        return NoContent();
    }

    [HttpGet]
    public async Task<IActionResult> List()
        => Ok(await _list.Handle());
}
