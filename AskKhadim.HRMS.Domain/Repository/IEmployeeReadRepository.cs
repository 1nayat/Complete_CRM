using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AskKhadim.HRMS.Domain.Employees;

namespace AskKhadim.HRMS.Domain.Repository;

public interface IEmployeeReadRepository
{
    Task<EmployeeProfileDto?> GetByIdAsync(long userId);
    Task<List<EmployeeProfileDto>> GetAllAsync();
}

