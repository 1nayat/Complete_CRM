using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using AskKhadim.HRMS.Infrastructure.Data;
using AskKhadim.HRMS.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace AskKhadim.HRMS.Infrastructure.Seed
{
    public static class SeedSuperAdminDirect
    {
        public static async Task EnsureSuperAdminAsync(IServiceProvider services)
        {
            using var scope = services.CreateScope();
            var provider = scope.ServiceProvider;
            var logger = provider.GetService<ILoggerFactory>()?.CreateLogger("SeedSuperAdminDirect");

            // 🔴 PROOF: Seeder execution
            Console.WriteLine(">>> SeedSuperAdminDirect EXECUTING <<<");

            try
            {
                var config = provider.GetService<IConfiguration>();
                var db = provider.GetRequiredService<AskKhadimDbContext>();

                // 🔴 PROOF: Which DB is EF connected to
                Console.WriteLine($"Seeder DB = {db.Database.GetDbConnection().Database}");

                string? superPassword = null;
                if (config != null)
                {
                    superPassword = config["SUPERADMIN_PASSWORD"] ?? config["Seed:SuperAdminPassword"];
                }

                if (string.IsNullOrWhiteSpace(superPassword))
                {
                    superPassword = Environment.GetEnvironmentVariable("SUPERADMIN_PASSWORD");
                }

                var usedFallback = false;
                if (string.IsNullOrWhiteSpace(superPassword))
                {
                    usedFallback = true;
                    logger?.LogWarning("SUPERADMIN_PASSWORD missing. Using fallback (DEV ONLY).");
                    superPassword = "superadmin";
                }

                var superEmail = "superadmin@askkhadim.com";
                var superEmployeeId = "SUPERADMIN";

                // ---------- ROLE ----------
                var superRole = await db.roles.FirstOrDefaultAsync(r => r.role_name == "SuperAdmin");
                if (superRole == null)
                {
                    Console.WriteLine("SuperAdmin role NOT found. Creating...");
                    superRole = new role
                    {
                        role_id = Guid.NewGuid(),
                        role_name = "SuperAdmin",
                        description = "System super administrator with full privileges",
                        created_at = DateTime.UtcNow
                    };
                    db.roles.Add(superRole);
                    await db.SaveChangesAsync();
                }
                else
                {
                    Console.WriteLine("SuperAdmin role already exists.");
                }

                // ---------- USER ----------
                var existing = await db.core_users.FirstOrDefaultAsync(u => u.email == superEmail);
                if (existing != null)
                {
                    Console.WriteLine("SuperAdmin user already exists.");

                    var mappingExists = await db.user_roles
                        .AnyAsync(ur => ur.user_id == existing.id && ur.role_id == superRole.role_id);

                    if (!mappingExists)
                    {
                        Console.WriteLine("Assigning SuperAdmin role to existing user.");
                        db.user_roles.Add(new user_role
                        {
                            user_id = existing.id,
                            role_id = superRole.role_id,
                            assigned_at = DateTime.UtcNow
                        });
                        await db.SaveChangesAsync();
                    }

                    return;
                }

                Console.WriteLine("Creating SuperAdmin user.");

                var hashed = BCrypt.Net.BCrypt.HashPassword(superPassword);

                var user = new core_user
                {
                    user_uuid = Guid.NewGuid(),
                    employee_id = superEmployeeId,
                    email = superEmail,
                    password_hash = hashed,
                    is_active = true,
                    email_verified = true,
                    created_at = DateTime.UtcNow,
                    updated_at = DateTime.UtcNow
                };

                db.core_users.Add(user);
                await db.SaveChangesAsync();

                db.user_roles.Add(new user_role
                {
                    user_id = user.id,
                    role_id = superRole.role_id,
                    assigned_at = DateTime.UtcNow
                });

                await db.SaveChangesAsync();

                Console.WriteLine(
                    $"Seeder DONE. Users={await db.core_users.CountAsync()}, Roles={await db.roles.CountAsync()}"
                );
            }
            catch (Exception ex)
            {
                Console.WriteLine("Seeder FAILED with exception.");
                var logger2 = services.GetService<ILoggerFactory>()?.CreateLogger("SeedSuperAdminDirect");
                logger2?.LogError(ex, "Error while seeding SuperAdmin user");
                throw;
            }
        }
    }
}
