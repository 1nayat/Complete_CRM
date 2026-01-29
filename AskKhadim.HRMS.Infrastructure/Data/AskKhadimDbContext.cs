using System;
using System.Reflection;
using System.Security;
using System.Threading;
using System.Threading.Tasks;
using AskKhadim.HRMS.Application.Common.Security;
using AskKhadim.HRMS.Domain.Common;
using AskKhadim.HRMS.Infrastructure.Models;
using AskKhadim.HRMS.Infrastructure.Persistence.Models;
using Microsoft.EntityFrameworkCore;

namespace AskKhadim.HRMS.Infrastructure.Data;

public partial class AskKhadimDbContext : DbContext
{
    private readonly ICurrentUser _currentUser;

    public AskKhadimDbContext(
        DbContextOptions<AskKhadimDbContext> options,
        ICurrentUser currentUser
    ) : base(options)
    {
        _currentUser = currentUser
            ?? throw new InvalidOperationException(
                "ICurrentUser is not available. DbContext must be created within an authenticated request."
            );
    }

    // -------------------- DbSets --------------------

    public DbSet<organization_invitation> organization_invitations { get; set; } = null!;
    public virtual DbSet<core_user> core_users { get; set; }
    public virtual DbSet<department> departments { get; set; }
    public virtual DbSet<user_appraisal_history> user_appraisal_histories { get; set; }
    public virtual DbSet<user_asset> user_assets { get; set; }
    public virtual DbSet<user_attendance> user_attendances { get; set; }
    public virtual DbSet<user_bank_details_secure> user_bank_details_secures { get; set; }
    public virtual DbSet<user_certification> user_certifications { get; set; }
    public virtual DbSet<user_document> user_documents { get; set; }
    public virtual DbSet<user_education> user_educations { get; set; }
    public virtual DbSet<user_employment_history> user_employment_histories { get; set; }
    public virtual DbSet<user_experience> user_experiences { get; set; }
    public virtual DbSet<user_health_insurance> user_health_insurances { get; set; }
    public virtual DbSet<user_hr> user_hrs { get; set; }
    public virtual DbSet<user_language> user_languages { get; set; }
    public virtual DbSet<user_leave_balance> user_leave_balances { get; set; }
    public virtual DbSet<user_leave_request> user_leave_requests { get; set; }
    public virtual DbSet<user_performance_rating> user_performance_ratings { get; set; }
    public virtual DbSet<user_profile> user_profiles { get; set; }
    public virtual DbSet<user_salary> user_salaries { get; set; }
    public virtual DbSet<user_sensitive_identifier> user_sensitive_identifiers { get; set; }
    public virtual DbSet<user_skill> user_skills { get; set; }
    public virtual DbSet<user_training_record> user_training_records { get; set; }

    public virtual DbSet<v_user_profile_basic> v_user_profile_basics { get; set; }
    public virtual DbSet<organization> organizations { get; set; }
    public virtual DbSet<organization_contact> organization_contacts { get; set; }
    public virtual DbSet<organization_address> organization_addresses { get; set; }
    public virtual DbSet<organization_file> organization_files { get; set; }
    public virtual DbSet<invitation> invitations { get; set; }
    public virtual DbSet<refresh_token> refresh_tokens { get; set; }
    public virtual DbSet<role> roles { get; set; }
    public virtual DbSet<user_role> user_roles { get; set; }
    public virtual DbSet<audit_log> audit_logs { get; set; }
    public virtual DbSet<legal_consent> legal_consents { get; set; }

    public DbSet<Permission> Permissions { get; set; }
    public DbSet<RolePermission> RolePermissions { get; set; }

    // -------------------- Model Config --------------------

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // ✅ FIX: RolePermission must have a key
        modelBuilder.Entity<RolePermission>(entity =>
        {
            entity.ToTable("role_permissions");
            entity.HasKey(e => new { e.RoleId, e.PermissionId });

            entity.HasOne<role>()
                  .WithMany()
                  .HasForeignKey(e => e.RoleId);

            entity.HasOne<Permission>()
                  .WithMany()
                  .HasForeignKey(e => e.PermissionId);
        });

        modelBuilder.Entity<v_user_profile_basic>()
            .HasNoKey()
            .ToView("v_user_profile_basic");

        ApplyOrganizationFilters(modelBuilder);

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    // -------------------- Org Filters --------------------

    private void ApplyOrganizationFilters(ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(OrgScopedEntity).IsAssignableFrom(entityType.ClrType))
            {
                var method = typeof(AskKhadimDbContext)
                    .GetMethod(nameof(SetOrgFilter),
                        BindingFlags.NonPublic | BindingFlags.Instance)!
                    .MakeGenericMethod(entityType.ClrType);

                method.Invoke(this, new object[] { modelBuilder });
            }
        }
    }

    private void SetOrgFilter<TEntity>(ModelBuilder modelBuilder)
        where TEntity : OrgScopedEntity
    {
        modelBuilder.Entity<TEntity>().HasQueryFilter(e =>
            _currentUser.IsSuperAdmin ||
            e.OrganizationId == _currentUser.OrganizationId
        );
    }

    // -------------------- Save Guards --------------------

    public override int SaveChanges()
    {
        EnforceOrgOwnership();
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        EnforceOrgOwnership();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void EnforceOrgOwnership()
    {
        if (_currentUser.IsSuperAdmin) return;

        foreach (var entry in ChangeTracker.Entries<OrgScopedEntity>())
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.OrganizationId =
                    _currentUser.OrganizationId
                    ?? throw new SecurityException("Org missing");
            }

            if (entry.State == EntityState.Modified)
            {
                var originalOrg =
                    entry.OriginalValues.GetValue<Guid>("OrganizationId");

                if (originalOrg != _currentUser.OrganizationId)
                    throw new SecurityException("Cross-org update blocked");
            }
        }
    }
}
