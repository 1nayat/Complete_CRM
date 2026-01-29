using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AskKhadim.HRMS.Domain.Security
{
    public static class PermissionCodes
    {
        public const string EmployeeCreate = "EMPLOYEE_CREATE";
        public const string EmployeeUpdate = "EMPLOYEE_UPDATE";
        public const string EmployeeDeactivate = "EMPLOYEE_DEACTIVATE";
        public const string EmployeeView = "EMPLOYEE_VIEW";

        // Department
        public const string DepartmentCreate = "DEPARTMENT_CREATE";
        public const string DepartmentUpdate = "DEPARTMENT_UPDATE";
        public const string DepartmentDelete = "DEPARTMENT_DELETE";
        public const string DepartmentView = "DEPARTMENT_VIEW";
    }

}
