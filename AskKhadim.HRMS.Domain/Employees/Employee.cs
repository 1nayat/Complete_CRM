using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace AskKhadim.HRMS.Domain.Employees;

public sealed class Employee
{
    public long UserId { get; private set; }
    public string EmployeeCode { get; }
    public string Email { get; }

    public string FirstName { get; private set; }
    public string LastName { get; private set; }

    public Guid DepartmentId { get; private set; }
    public string Designation { get; private set; }

    public bool IsActive { get; private set; }

    private Employee() { } // safety

    public Employee(
        string employeeCode,
        string email,
        string firstName,
        string lastName,
        Guid departmentId,
        string designation)
    {
        EmployeeCode = employeeCode;
        Email = email;
        FirstName = firstName;
        LastName = lastName;
        DepartmentId = departmentId;
        Designation = designation;
        IsActive = true;
    }

    public void Update(
        string firstName,
        string lastName,
        Guid departmentId,
        string designation)
    {
        FirstName = firstName;
        LastName = lastName;
        DepartmentId = departmentId;
        Designation = designation;
    }

    public void Deactivate()
    {
        IsActive = false;
    }
}
