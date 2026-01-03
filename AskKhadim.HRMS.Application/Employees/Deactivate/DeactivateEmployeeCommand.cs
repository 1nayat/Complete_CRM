using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AskKhadim.HRMS.Application.Employees.Deactivate;

public sealed record DeactivateEmployeeCommand(long UserId);
