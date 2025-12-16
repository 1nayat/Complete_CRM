using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AskKhadim.HRMS.Infrastructure.Models;

[Table("user_hr")]
[Index("department_id", Name = "IX_user_hr_department")]
[Index("designation", Name = "IX_user_hr_designation")]
[Index("employment_status", Name = "IX_user_hr_employment_status")]
[Index("employment_type", Name = "IX_user_hr_employment_type")]
[Index("joining_date", Name = "IX_user_hr_joining_date")]
[Index("probation_end_date", Name = "IX_user_hr_probation_end")]
[Index("reporting_manager_id", Name = "IX_user_hr_reporting_manager")]
public partial class user_hr
{
    [Key]
    public long user_id { get; set; }

    public Guid? user_uuid { get; set; }

    public Guid? department_id { get; set; }

    [StringLength(150)]
    [Unicode(false)]
    public string? designation { get; set; }

    public long? reporting_manager_id { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string? work_location { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string work_type { get; set; } = null!;

    [StringLength(20)]
    [Unicode(false)]
    public string employment_type { get; set; } = null!;

    [StringLength(20)]
    [Unicode(false)]
    public string employment_status { get; set; } = null!;

    public DateOnly? onboarding_date { get; set; }

    public DateOnly? joining_date { get; set; }

    public DateOnly? probation_start_date { get; set; }

    public DateOnly? probation_end_date { get; set; }

    public DateOnly? confirmation_date { get; set; }

    public DateOnly? exit_date { get; set; }

    [Column(TypeName = "decimal(5, 2)")]
    public decimal prior_total_experience_years { get; set; }

    [Column(TypeName = "decimal(5, 2)")]
    public decimal prior_relevant_experience_years { get; set; }

    [Column(TypeName = "numeric(18, 6)")]
    public decimal? total_experience_years { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string access_type { get; set; } = null!;

    public int access_level { get; set; }

    [Precision(3)]
    public DateTime created_at { get; set; }

    [Precision(3)]
    public DateTime? updated_at { get; set; }

    [ForeignKey("department_id")]
    [InverseProperty("user_hrs")]
    public virtual department? department { get; set; }

    [ForeignKey("reporting_manager_id")]
    [InverseProperty("user_hrreporting_managers")]
    public virtual core_user? reporting_manager { get; set; }

    [ForeignKey("user_id")]
    [InverseProperty("user_hruser")]
    public virtual core_user user { get; set; } = null!;
}
