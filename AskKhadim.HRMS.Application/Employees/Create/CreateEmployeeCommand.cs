using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public sealed record CreateEmployeeCommand(
    string EmployeeCode,
    string Email,
    string FirstName,
    string LastName,
    Guid DepartmentId,
    string Designation,
    DateTime JoiningDate
);
