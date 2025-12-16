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
        // Call this from Program.cs during startup after DbContext is registered:
        // using (var scope = app.Services.CreateScope()) { await SeedSuperAdminDirect.EnsureSuperAdminAsync(scope.ServiceProvider); }
        public static async Task EnsureSuperAdminAsync(IServiceProvider services)
        {
            using var scope = services.CreateScope();
            var provider = scope.ServiceProvider;
            var logger = provider.GetService<ILoggerFactory>()?.CreateLogger("SeedSuperAdminDirect");

            try
            {
                var config = provider.GetService<IConfiguration>();
                var db = provider.GetRequiredService<AskKhadimDbContext>();

                string ? superPassword = null;
                if (config != null)
                {
                    superPassword = config["SUPERADMIN_PASSWORD"] ?? config["Seed:SuperAdminPassword"]!;
                }

                if (string.IsNullOrWhiteSpace(superPassword))
                {
                    superPassword = Environment.GetEnvironmentVariable("SUPERADMIN_PASSWORD")!;
                }

                var usedFallback = false;
                if (string.IsNullOrWhiteSpace(superPassword))
                {
                    usedFallback = true;
                    logger?.LogWarning("SUPERADMIN_PASSWORD not provided via configuration or environment. Using insecure fallback password (dev only). Change immediately in production.");
                    superPassword = "superadmin"; 
                }
                else
                {
                    logger?.LogInformation("SUPERADMIN_PASSWORD found in configuration/environment. (value not logged)");
                }

                var superEmail = "superadmin@askkhadim.com";
                var superUsername = "superadmin";
                var superEmployeeId = "SUPERADMIN";

                // Ensure Role exists
                var superRole = await db.roles.FirstOrDefaultAsync(r => r.role_name == "SuperAdmin");
                if (superRole == null)
                {
                    logger?.LogInformation("SuperAdmin role not found — creating it now.");
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

                var existing = await db.core_users.FirstOrDefaultAsync(u => u.email == superEmail);
                if (existing != null)
                {
                    logger?.LogInformation("SuperAdmin already exists with email {Email} (id={Id}).", superEmail, existing.id);

                    var mappingExists = await db.user_roles.AnyAsync(ur => ur.user_id == existing.id && ur.role_id == superRole.role_id);
                    if (!mappingExists)
                    {
                        db.user_roles.Add(new user_role
                        {
                            user_id = existing.id,
                            role_id = superRole.role_id,
                            organization_id = null,
                            assigned_at = DateTime.UtcNow,
                            assigned_by = null
                        });
                        await db.SaveChangesAsync();
                        logger?.LogInformation("Assigned SuperAdmin role to existing user {Email}.", superEmail);
                    }

                    return;
                }

                var hashed = BCrypt.Net.BCrypt.HashPassword(superPassword);

                var user = new core_user
                {
                    user_uuid = Guid.NewGuid(),
                    employee_id = superEmployeeId,
                    email = superEmail,
                    password_hash = hashed,    
                    is_active = true,
                    email_verified = true,
                    notice_period_days = 0,
                    linkedin_profile_url = null,
                    last_login = null,
                    created_at = DateTime.UtcNow,
                    updated_at = DateTime.UtcNow
                };

                db.core_users.Add(user);
                await db.SaveChangesAsync(); 

                db.user_roles.Add(new user_role
                {
                    user_id = user.id,
                    role_id = superRole.role_id,
                    organization_id = null,
                    assigned_at = DateTime.UtcNow,
                    assigned_by = null
                });

                await db.SaveChangesAsync();

                db.audit_logs.Add(new audit_log
                {
                    actor_user_id = user.id,
                    actor_role = "SuperAdmin",
                    action_type = "SeedCreate",
                    entity_type = "core_user",
                    entity_id = user.id.ToString(),
                    old_value = null,
                    new_value = $"Created superadmin {superEmail}",
                    timestamp = DateTime.UtcNow,
                    correlation_id = Guid.NewGuid()
                });

                await db.SaveChangesAsync();

                logger?.LogInformation("SuperAdmin user created and role assigned. Email={Email}, id={Id}. FallbackPasswordUsed={Fallback}", superEmail, user.id, usedFallback);
            }
            catch (Exception ex)
            {
                var logger2 = services.GetService<ILoggerFactory>()?.CreateLogger("SeedSuperAdminDirect");
                logger2?.LogError(ex, "Error while seeding SuperAdmin user");
                throw;
            }
        }
    }
}
