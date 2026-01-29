using AskKhadim.HRMS.Domain.Security;

namespace AskKhadim.HRMS.Api.Authorization
{
    public static class PermissionPolicies
    {
        public static void Register(IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy(
                    PermissionCodes.EmployeeCreate,
                    p => p.RequireClaim("permission", PermissionCodes.EmployeeCreate));

                options.AddPolicy(
                    PermissionCodes.EmployeeUpdate,
                    p => p.RequireClaim("permission", PermissionCodes.EmployeeUpdate));

                options.AddPolicy(
                    PermissionCodes.EmployeeDeactivate,
                    p => p.RequireClaim("permission", PermissionCodes.EmployeeDeactivate));

                options.AddPolicy(
                    PermissionCodes.EmployeeView,
                    p => p.RequireClaim("permission", PermissionCodes.EmployeeView));

                // Department
                options.AddPolicy(
                    PermissionCodes.DepartmentCreate,
                    p => p.RequireClaim("permission", PermissionCodes.DepartmentCreate));

                options.AddPolicy(
                    PermissionCodes.DepartmentUpdate,
                    p => p.RequireClaim("permission", PermissionCodes.DepartmentUpdate));

                options.AddPolicy(
                    PermissionCodes.DepartmentDelete,
                    p => p.RequireClaim("permission", PermissionCodes.DepartmentDelete));

                options.AddPolicy(
                    PermissionCodes.DepartmentView,
                    p => p.RequireClaim("permission", PermissionCodes.DepartmentView));

            });
        }
    }

}
