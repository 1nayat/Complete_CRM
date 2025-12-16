using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AskKhadim.HRMS.Infrastructure.Models;

[Table("user_experience")]
[Index("is_current", Name = "IX_user_experience_current")]
[Index("start_date", "end_date", Name = "IX_user_experience_dates")]
[Index("user_id", Name = "IX_user_experience_user")]
public partial class user_experience
{
    [Key]
    public Guid experience_id { get; set; }

    public long user_id { get; set; }

    [StringLength(250)]
    [Unicode(false)]
    public string company_name { get; set; } = null!;

    [StringLength(150)]
    [Unicode(false)]
    public string designation { get; set; } = null!;

    [StringLength(150)]
    [Unicode(false)]
    public string? department { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? employment_type { get; set; }

    public DateOnly start_date { get; set; }

    public DateOnly? end_date { get; set; }

    public bool is_current { get; set; }

    [Column(TypeName = "numeric(17, 6)")]
    public decimal? duration_years { get; set; }

    public string? job_description { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string? reporting_to { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string? reason_for_leaving { get; set; }

    [Column(TypeName = "decimal(14, 2)")]
    public decimal? salary_drawn { get; set; }

    [StringLength(3)]
    [Unicode(false)]
    public string currency { get; set; } = null!;

    [Precision(3)]
    public DateTime created_at { get; set; }

    [ForeignKey("user_id")]
    [InverseProperty("user_experiences")]
    public virtual core_user user { get; set; } = null!;
}
