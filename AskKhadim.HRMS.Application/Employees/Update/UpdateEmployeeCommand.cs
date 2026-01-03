using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AskKhadim.HRMS.Domain.Employees;

namespace AskKhadim.HRMS.Application.Employees.Update;

public sealed record UpdateEmployeeCommand(
    long UserId,
    string FirstName,
    string LastName,
    Guid DepartmentId,
    string Designation
);
