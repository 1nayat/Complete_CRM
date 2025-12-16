using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AskKhadim.HRMS.Infrastructure.Models;

[Table("user_education")]
[Index("end_year", Name = "IX_user_education_end_year")]
[Index("user_id", Name = "IX_user_education_user")]
public partial class user_education
{
    [Key]
    public Guid education_id { get; set; }

    public long user_id { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string degree_type { get; set; } = null!;

    [StringLength(200)]
    [Unicode(false)]
    public string degree_name { get; set; } = null!;

    [StringLength(200)]
    [Unicode(false)]
    public string? specialization { get; set; }

    [StringLength(300)]
    [Unicode(false)]
    public string institution_name { get; set; } = null!;

    [StringLength(300)]
    [Unicode(false)]
    public string? university_name { get; set; }

    public short? start_year { get; set; }

    public short? end_year { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? grade_type { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? grade_value { get; set; }

    public bool is_highest { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string? certificate_url { get; set; }

    [Precision(3)]
    public DateTime created_at { get; set; }

    [ForeignKey("user_id")]
    [InverseProperty("user_educations")]
    public virtual core_user user { get; set; } = null!;
}
