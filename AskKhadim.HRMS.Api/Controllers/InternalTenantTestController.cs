using AskKhadim.HRMS.Api.Dtos;
using AskKhadim.HRMS.Infrastructure.Data;
using AskKhadim.HRMS.Infrastructure.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AskKhadim.HRMS.Api.Controllers;

[ApiController]
[Route("api/departments")]
[Authorize]
public class DepartmentsController : ControllerBase
{
    private readonly AskKhadimDbContext _db;

    public DepartmentsController(AskKhadimDbContext db)
    {
        _db = db;
    }

    // =====================================================
    // CREATE DEPARTMENT
    // =====================================================
    [HttpPost]
    public async Task<IActionResult> CreateDepartment([FromBody] CreateDepartmentDto dto)
    {
        if (dto.ParentDepartmentId.HasValue)
        {
            var parentExists = await _db.departments
                .AnyAsync(d => d.department_id == dto.ParentDepartmentId.Value && d.is_active);

            if (!parentExists)
                return BadRequest("Parent department does not exist or is not accessible.");
        }

        var department = new department
        {
            department_id = Guid.NewGuid(),
            department_code = dto.DepartmentCode,
            department_name = dto.DepartmentName,
            description = dto.Description,
            parent_department_id = dto.ParentDepartmentId,
            cost_center = dto.CostCenter,
            location = dto.Location,
            is_active = true
        };

        _db.departments.Add(department);
        await _db.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetDepartmentById),
            new { id = department.department_id },
            new
            {
                department.department_id,
                department.department_code,
                department.department_name,
                department.parent_department_id,
                department.cost_center,
                department.location
            }
        );
    }

    // =====================================================
    // GET ALL DEPARTMENTS (ORG FILTER AUTO-APPLIED)
    // =====================================================
    [HttpGet]
    public async Task<IActionResult> GetDepartments()
    {
        var departments = await _db.departments
            .Where(d => d.is_active)
            .Select(d => new
            {
                d.department_id,
                d.department_code,
                d.department_name,
                d.parent_department_id,
                d.cost_center,
                d.location
            })
            .ToListAsync();

        return Ok(departments);
    }

    // =====================================================
    // GET DEPARTMENT BY ID
    // =====================================================
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetDepartmentById(Guid id)
    {
        var department = await _db.departments
            .Where(d => d.department_id == id && d.is_active)
            .Select(d => new
            {
                d.department_id,
                d.department_code,
                d.department_name,
                d.description,
                d.parent_department_id,
                d.cost_center,
                d.location
            })
            .FirstOrDefaultAsync();

        if (department == null)
            return NotFound();

        return Ok(department);
    }

    // =====================================================
    // UPDATE DEPARTMENT
    // =====================================================
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateDepartment(
        Guid id,
        [FromBody] UpdateDepartmentDto dto)
    {
        var department = await _db.departments
            .FirstOrDefaultAsync(d => d.department_id == id && d.is_active);

        if (department == null)
            return NotFound();

        if (dto.ParentDepartmentId.HasValue)
        {
            var parentExists = await _db.departments
                .AnyAsync(d => d.department_id == dto.ParentDepartmentId.Value && d.is_active);

            if (!parentExists)
                return BadRequest("Parent department does not exist or is not accessible.");
        }

        department.department_name = dto.DepartmentName;
        department.description = dto.Description;
        department.cost_center = dto.CostCenter;
        department.location = dto.Location;
        department.parent_department_id = dto.ParentDepartmentId;
        department.updated_at = DateTime.UtcNow;

        await _db.SaveChangesAsync();

        return Ok(new
        {
            department.department_id,
            department.department_code,
            department.department_name,
            department.parent_department_id,
            department.cost_center,
            department.location
        });
    }

    // =====================================================
    // SOFT DELETE (DEACTIVATE)
    // =====================================================
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeactivateDepartment(Guid id)
    {
        var department = await _db.departments
            .FirstOrDefaultAsync(d => d.department_id == id && d.is_active);

        if (department == null)
            return NotFound();

        department.is_active = false;
        department.updated_at = DateTime.UtcNow;

        await _db.SaveChangesAsync();

        return NoContent();
    }
}
