using System;
using System.Collections.Generic;
using AskKhadim.HRMS.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace AskKhadim.HRMS.Infrastructure.Data;

public partial class AskKhadimDbContext : DbContext
{
    public AskKhadimDbContext()
    {
    }

    public AskKhadimDbContext(DbContextOptions<AskKhadimDbContext> options)
        : base(options)
    {
    }
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


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-VTQVDQ0;Database=askkhadim_hrms;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<core_user>(entity =>
        {
            entity.Property(e => e.created_at).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.is_active).HasDefaultValue(true);
            entity.Property(e => e.updated_at).HasDefaultValueSql("(sysutcdatetime())");
        });

        modelBuilder.Entity<department>(entity =>
        {
            entity.Property(e => e.department_id).ValueGeneratedNever();
            entity.Property(e => e.created_at).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.is_active).HasDefaultValue(true);

            entity.HasOne(d => d.department_head).WithMany(p => p.departments)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_departments_head");

            entity.HasOne(d => d.parent_department).WithMany(p => p.Inverseparent_department).HasConstraintName("FK_departments_parent");
        });

        modelBuilder.Entity<user_appraisal_history>(entity =>
        {
            entity.Property(e => e.appraisal_id).ValueGeneratedNever();
            entity.Property(e => e.created_at).HasDefaultValueSql("(sysutcdatetime())");

            entity.HasOne(d => d.appraised_byNavigation).WithMany(p => p.user_appraisal_historyappraised_byNavigations).HasConstraintName("FK_user_appraisal_history_appraised_by");

            entity.HasOne(d => d.user).WithMany(p => p.user_appraisal_historyusers).HasConstraintName("FK_user_appraisal_history_user");
        });

        modelBuilder.Entity<user_asset>(entity =>
        {
            entity.HasIndex(e => e.asset_tag, "UQ_user_assets_asset_tag")
                .IsUnique()
                .HasFilter("([asset_tag] IS NOT NULL)");

            entity.HasIndex(e => e.serial_number, "UQ_user_assets_serial_number")
                .IsUnique()
                .HasFilter("([serial_number] IS NOT NULL)");

            entity.Property(e => e.asset_id).ValueGeneratedNever();
            entity.Property(e => e.created_at).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.status).HasDefaultValue("Assigned");

            entity.HasOne(d => d.user).WithMany(p => p.user_assets).HasConstraintName("FK_user_assets_user");
        });

        modelBuilder.Entity<user_attendance>(entity =>
        {
            entity.Property(e => e.attendance_id).ValueGeneratedNever();
            entity.Property(e => e.created_at).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.status).HasDefaultValue("Present");

            entity.HasOne(d => d.user).WithMany(p => p.user_attendances).HasConstraintName("FK_user_attendance_user");
        });

        modelBuilder.Entity<user_bank_details_secure>(entity =>
        {
            entity.Property(e => e.bank_detail_id).ValueGeneratedNever();
            entity.Property(e => e.created_at).HasDefaultValueSql("(sysutcdatetime())");

            entity.HasOne(d => d.user).WithMany(p => p.user_bank_details_secures).HasConstraintName("FK_user_bank_details_secure_core_users");
        });

        modelBuilder.Entity<user_certification>(entity =>
        {
            entity.Property(e => e.certification_id).ValueGeneratedNever();
            entity.Property(e => e.created_at).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.is_active).HasDefaultValue(true);

            entity.HasOne(d => d.user).WithMany(p => p.user_certifications).HasConstraintName("FK_user_certifications_user");
        });

        modelBuilder.Entity<user_document>(entity =>
        {
            entity.Property(e => e.document_id).ValueGeneratedNever();
            entity.Property(e => e.upload_date).HasDefaultValueSql("(sysutcdatetime())");

            entity.HasOne(d => d.uploaded_byNavigation).WithMany(p => p.user_documentuploaded_byNavigations).HasConstraintName("FK_user_documents_uploaded_by");

            entity.HasOne(d => d.user).WithMany(p => p.user_documentusers).HasConstraintName("FK_user_documents_user");

            entity.HasOne(d => d.verified_byNavigation).WithMany(p => p.user_documentverified_byNavigations).HasConstraintName("FK_user_documents_verified_by");
        });

        modelBuilder.Entity<user_education>(entity =>
        {
            entity.Property(e => e.education_id).ValueGeneratedNever();
            entity.Property(e => e.created_at).HasDefaultValueSql("(sysutcdatetime())");

            entity.HasOne(d => d.user).WithMany(p => p.user_educations).HasConstraintName("FK_user_education_user");
        });

        modelBuilder.Entity<user_employment_history>(entity =>
        {
            entity.Property(e => e.history_id).ValueGeneratedNever();
            entity.Property(e => e.created_at).HasDefaultValueSql("(sysutcdatetime())");

            entity.HasOne(d => d.changed_byNavigation).WithMany(p => p.user_employment_historychanged_byNavigations).HasConstraintName("FK_user_employment_history_changed_by");

            entity.HasOne(d => d.department).WithMany(p => p.user_employment_histories).HasConstraintName("FK_user_employment_history_department");

            entity.HasOne(d => d.reporting_manager).WithMany(p => p.user_employment_historyreporting_managers).HasConstraintName("FK_user_employment_history_reporting_manager");

            entity.HasOne(d => d.user).WithMany(p => p.user_employment_historyusers).HasConstraintName("FK_user_employment_history_user");
        });

        modelBuilder.Entity<user_experience>(entity =>
        {
            entity.Property(e => e.experience_id).ValueGeneratedNever();
            entity.Property(e => e.created_at).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.currency)
                .HasDefaultValue("INR")
                .IsFixedLength();
            entity.Property(e => e.duration_years).HasComputedColumnSql("(datediff(month,[start_date],isnull([end_date],CONVERT([date],sysutcdatetime())))/(12.0))", false);

            entity.HasOne(d => d.user).WithMany(p => p.user_experiences).HasConstraintName("FK_user_experience_user");
        });

        modelBuilder.Entity<user_health_insurance>(entity =>
        {
            entity.Property(e => e.health_insurance_id).ValueGeneratedNever();
            entity.Property(e => e.created_at).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.is_active).HasDefaultValue(true);

            entity.HasOne(d => d.user).WithMany(p => p.user_health_insurances).HasConstraintName("FK_user_health_insurance_user");
        });

        modelBuilder.Entity<user_hr>(entity =>
        {
            entity.Property(e => e.user_id).ValueGeneratedNever();
            entity.Property(e => e.access_level).HasDefaultValue(1);
            entity.Property(e => e.access_type).HasDefaultValue("Limited Access");
            entity.Property(e => e.created_at).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.employment_status).HasDefaultValue("Active");
            entity.Property(e => e.employment_type).HasDefaultValue("Full Time");
            entity.Property(e => e.total_experience_years).HasComputedColumnSql("([prior_total_experience_years]+isnull(datediff(month,[joining_date],isnull([exit_date],CONVERT([date],sysutcdatetime())))/(12.0),(0)))", false);
            entity.Property(e => e.work_type).HasDefaultValue("On Site");

            entity.HasOne(d => d.department).WithMany(p => p.user_hrs)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_user_hr_departments");

            entity.HasOne(d => d.reporting_manager).WithMany(p => p.user_hrreporting_managers).HasConstraintName("FK_user_hr_reporting_manager");

            entity.HasOne(d => d.user).WithOne(p => p.user_hruser).HasConstraintName("FK_user_hr_core_users");
        });

        modelBuilder.Entity<user_language>(entity =>
        {
            entity.Property(e => e.language_id).ValueGeneratedNever();
            entity.Property(e => e.created_at).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.proficiency).HasDefaultValue("Basic");

            entity.HasOne(d => d.user).WithMany(p => p.user_languages).HasConstraintName("FK_user_languages_user");
        });

        modelBuilder.Entity<user_leave_balance>(entity =>
        {
            entity.Property(e => e.leave_balance_id).ValueGeneratedNever();
            entity.Property(e => e.updated_at).HasDefaultValueSql("(sysutcdatetime())");

            entity.HasOne(d => d.user).WithMany(p => p.user_leave_balances).HasConstraintName("FK_user_leave_balance_user");
        });

        modelBuilder.Entity<user_leave_request>(entity =>
        {
            entity.Property(e => e.leave_request_id).ValueGeneratedNever();
            entity.Property(e => e.requested_at).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.status).HasDefaultValue("Pending");

            entity.HasOne(d => d.processed_byNavigation).WithMany(p => p.user_leave_requestprocessed_byNavigations).HasConstraintName("FK_user_leave_requests_processed_by");

            entity.HasOne(d => d.user).WithMany(p => p.user_leave_requestusers).HasConstraintName("FK_user_leave_requests_user");
        });

        modelBuilder.Entity<user_performance_rating>(entity =>
        {
            entity.Property(e => e.rating_id).ValueGeneratedNever();
            entity.Property(e => e.created_at).HasDefaultValueSql("(sysutcdatetime())");

            entity.HasOne(d => d.rated_byNavigation).WithMany(p => p.user_performance_ratingrated_byNavigations).HasConstraintName("FK_user_performance_ratings_rated_by");

            entity.HasOne(d => d.user).WithMany(p => p.user_performance_ratingusers).HasConstraintName("FK_user_performance_ratings_user");
        });

        modelBuilder.Entity<user_profile>(entity =>
        {
            entity.Property(e => e.user_id).ValueGeneratedNever();
            entity.Property(e => e.created_at).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.current_country).HasDefaultValue("India");
            entity.Property(e => e.permanent_country).HasDefaultValue("India");

            entity.HasOne(d => d.user).WithOne(p => p.user_profile).HasConstraintName("FK_user_profile_core_users");
        });

        modelBuilder.Entity<user_salary>(entity =>
        {
            entity.Property(e => e.salary_id).ValueGeneratedNever();
            entity.Property(e => e.created_at).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.currency)
                .HasDefaultValue("INR")
                .IsFixedLength();

            entity.HasOne(d => d.user).WithMany(p => p.user_salaries).HasConstraintName("FK_user_salary_core_users");
        });

        modelBuilder.Entity<user_sensitive_identifier>(entity =>
        {
            entity.HasIndex(e => e.aadhaar_hash, "UX_user_sensitive_identifiers_aadhaar_hash")
                .IsUnique()
                .HasFilter("([aadhaar_hash] IS NOT NULL)");

            entity.HasIndex(e => e.pan_hash, "UX_user_sensitive_identifiers_pan_hash")
                .IsUnique()
                .HasFilter("([pan_hash] IS NOT NULL)");

            entity.Property(e => e.user_id).ValueGeneratedNever();
            entity.Property(e => e.created_at).HasDefaultValueSql("(sysutcdatetime())");

            entity.HasOne(d => d.user).WithOne(p => p.user_sensitive_identifier).HasConstraintName("FK_user_sensitive_identifiers_core_users");
        });

        modelBuilder.Entity<user_skill>(entity =>
        {
            entity.Property(e => e.skill_id).ValueGeneratedNever();
            entity.Property(e => e.created_at).HasDefaultValueSql("(sysutcdatetime())");

            entity.HasOne(d => d.user).WithMany(p => p.user_skills).HasConstraintName("FK_user_skills_user");
        });

        modelBuilder.Entity<user_training_record>(entity =>
        {
            entity.Property(e => e.training_id).ValueGeneratedNever();
            entity.Property(e => e.completion_status).HasDefaultValue("Planned");
            entity.Property(e => e.created_at).HasDefaultValueSql("(sysutcdatetime())");

            entity.HasOne(d => d.user).WithMany(p => p.user_training_records).HasConstraintName("FK_user_training_records_user");
        });

        modelBuilder.Entity<v_user_profile_basic>(entity =>
        {
            entity.ToView("v_user_profile_basic");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
