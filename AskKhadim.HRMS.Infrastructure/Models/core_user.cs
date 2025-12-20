using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AskKhadim.HRMS.Infrastructure.Models;

[Index(nameof(organization_id), nameof(email), IsUnique = true, Name = "UQ_core_users_org_email")]
[Index(nameof(organization_id), nameof(employee_id), IsUnique = true, Name = "UQ_core_users_org_employee")]
[Index(nameof(organization_id), Name = "IX_core_users_org")]
[Index(nameof(last_login), Name = "IX_core_users_last_login")]
[Index(nameof(user_uuid), IsUnique = true, Name = "UQ_core_users_user_uuid")]

public partial class core_user
{
    [Key]
    public long id { get; set; }

    public Guid user_uuid { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string employee_id { get; set; } = null!;

    [StringLength(150)]
    [Unicode(false)]
    public string email { get; set; } = null!;

    [StringLength(255)]
    [Unicode(false)]
    public string password_hash { get; set; } = null!;

    public bool is_active { get; set; }

    public bool email_verified { get; set; }

    [Precision(3)]
    public DateTime? last_login { get; set; }

    public int notice_period_days { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string? linkedin_profile_url { get; set; }

    [Precision(3)]
    public DateTime created_at { get; set; }

    [Precision(3)]
    public DateTime updated_at { get; set; }

    public Guid ? organization_id { get; set; }

    [StringLength(500)]
    public string? security_question { get; set; }

    [StringLength(500)]
    public string? security_answer_hash { get; set; }

    [StringLength(50)]
    public string? two_fa_preference { get; set; }

    [InverseProperty("department_head")]
    public virtual ICollection<department> departments { get; set; } = new List<department>();

    [ForeignKey("organization_id")]
    [InverseProperty("core_users")]
    public virtual organization? organization { get; set; }

    [InverseProperty("user")]
    public virtual ICollection<refresh_token> refresh_tokens { get; set; } = new List<refresh_token>();

    [InverseProperty("appraised_byNavigation")]
    public virtual ICollection<user_appraisal_history> user_appraisal_historyappraised_byNavigations { get; set; } = new List<user_appraisal_history>();

    [InverseProperty("user")]
    public virtual ICollection<user_appraisal_history> user_appraisal_historyusers { get; set; } = new List<user_appraisal_history>();

    [InverseProperty("user")]
    public virtual ICollection<user_asset> user_assets { get; set; } = new List<user_asset>();

    [InverseProperty("user")]
    public virtual ICollection<user_attendance> user_attendances { get; set; } = new List<user_attendance>();

    [InverseProperty("user")]
    public virtual ICollection<user_bank_details_secure> user_bank_details_secures { get; set; } = new List<user_bank_details_secure>();

    [InverseProperty("user")]
    public virtual ICollection<user_certification> user_certifications { get; set; } = new List<user_certification>();

    [InverseProperty("uploaded_byNavigation")]
    public virtual ICollection<user_document> user_documentuploaded_byNavigations { get; set; } = new List<user_document>();

    [InverseProperty("user")]
    public virtual ICollection<user_document> user_documentusers { get; set; } = new List<user_document>();

    [InverseProperty("verified_byNavigation")]
    public virtual ICollection<user_document> user_documentverified_byNavigations { get; set; } = new List<user_document>();

    [InverseProperty("user")]
    public virtual ICollection<user_education> user_educations { get; set; } = new List<user_education>();

    [InverseProperty("changed_byNavigation")]
    public virtual ICollection<user_employment_history> user_employment_historychanged_byNavigations { get; set; } = new List<user_employment_history>();

    [InverseProperty("reporting_manager")]
    public virtual ICollection<user_employment_history> user_employment_historyreporting_managers { get; set; } = new List<user_employment_history>();

    [InverseProperty("user")]
    public virtual ICollection<user_employment_history> user_employment_historyusers { get; set; } = new List<user_employment_history>();

    [InverseProperty("user")]
    public virtual ICollection<user_experience> user_experiences { get; set; } = new List<user_experience>();

    [InverseProperty("user")]
    public virtual ICollection<user_health_insurance> user_health_insurances { get; set; } = new List<user_health_insurance>();

    [InverseProperty("reporting_manager")]
    public virtual ICollection<user_hr> user_hrreporting_managers { get; set; } = new List<user_hr>();

    [InverseProperty("user")]
    public virtual user_hr? user_hruser { get; set; }

    [InverseProperty("user")]
    public virtual ICollection<user_language> user_languages { get; set; } = new List<user_language>();

    [InverseProperty("user")]
    public virtual ICollection<user_leave_balance> user_leave_balances { get; set; } = new List<user_leave_balance>();

    [InverseProperty("processed_byNavigation")]
    public virtual ICollection<user_leave_request> user_leave_requestprocessed_byNavigations { get; set; } = new List<user_leave_request>();

    [InverseProperty("user")]
    public virtual ICollection<user_leave_request> user_leave_requestusers { get; set; } = new List<user_leave_request>();

    [InverseProperty("rated_byNavigation")]
    public virtual ICollection<user_performance_rating> user_performance_ratingrated_byNavigations { get; set; } = new List<user_performance_rating>();

    [InverseProperty("user")]
    public virtual ICollection<user_performance_rating> user_performance_ratingusers { get; set; } = new List<user_performance_rating>();

    [InverseProperty("user")]
    public virtual user_profile? user_profile { get; set; }

    [InverseProperty("user")]
    public virtual ICollection<user_role> user_roles { get; set; } = new List<user_role>();

    [InverseProperty("user")]
    public virtual ICollection<user_salary> user_salaries { get; set; } = new List<user_salary>();

    [InverseProperty("user")]
    public virtual user_sensitive_identifier? user_sensitive_identifier { get; set; }

    [InverseProperty("user")]
    public virtual ICollection<user_skill> user_skills { get; set; } = new List<user_skill>();

    [InverseProperty("user")]
    public virtual ICollection<user_training_record> user_training_records { get; set; } = new List<user_training_record>();
}
