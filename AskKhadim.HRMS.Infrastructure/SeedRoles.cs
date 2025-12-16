using System;
using System.Linq;
using System.Threading.Tasks;
using AskKhadim.HRMS.Infrastructure.Data;
using AskKhadim.HRMS.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

public static class SeedRoles
{
    public static async Task EnsureRolesAsync(IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AskKhadimDbContext>();
        var logger = scope.ServiceProvider.GetService<ILoggerFactory>()?.CreateLogger("SeedRoles");

        var required = new[] {
            (Name: "SuperAdmin", Desc: "System super administrator with full privileges"),
            (Name: "ClientAdmin", Desc: "Administrator for a tenant/organization"),
            (Name: "Manager", Desc: "Manager role with team-level permissions"),
            (Name: "Employee", Desc: "Regular employee role")
        };

        foreach (var r in required)
        {
            var exists = await db.roles.AnyAsync(x => x.role_name == r.Name);
            if (!exists)
            {
                db.roles.Add(new role
                {
                    role_id = Guid.NewGuid(),
                    role_name = r.Name,
                    description = r.Desc,
                    created_at = DateTime.UtcNow
                });
                logger?.LogInformation("Seeding role {Role}", r.Name);
            }
        }

        await db.SaveChangesAsync();
    }
}
