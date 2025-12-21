using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AskKhadim.HRMS.Domain.Employees;

namespace AskKhadim.HRMS.Domain.Repository
{
    public interface IEmployeeRepository
    {
        Task<long> CreateAsync(Employee employee, DateTime joiningDate);
        Task UpdateAsync(long userId, Employee employee);
        Task DeactivateAsync(long userId);
    }
}
