using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AskKhadim.HRMS.Domain.Employees;

public sealed class EmployeeProfileDto
{
    public long UserId { get; init; }
    public string EmployeeCode { get; init; } = null!;
    public string Email { get; init; } = null!;
    public string FullName { get; init; } = null!;
    public string Department { get; init; } = null!;
    public string Designation { get; init; } = null!;
    public string EmploymentStatus { get; init; } = null!;
    public bool IsActive { get; init; }
}

