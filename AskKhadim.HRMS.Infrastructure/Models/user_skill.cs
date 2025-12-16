using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AskKhadim.HRMS.Infrastructure.Models;

[Index("skill_name", Name = "IX_user_skills_skill_name")]
[Index("user_id", Name = "IX_user_skills_user")]
public partial class user_skill
{
    [Key]
    public Guid skill_id { get; set; }

    public long user_id { get; set; }

    [StringLength(150)]
    [Unicode(false)]
    public string skill_name { get; set; } = null!;

    [StringLength(100)]
    [Unicode(false)]
    public string? skill_category { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? proficiency_level { get; set; }

    [Column(TypeName = "decimal(5, 2)")]
    public decimal? years_of_experience { get; set; }

    public short? last_used_year { get; set; }

    public bool is_primary { get; set; }

    [Precision(3)]
    public DateTime created_at { get; set; }

    [ForeignKey("user_id")]
    [InverseProperty("user_skills")]
    public virtual core_user user { get; set; } = null!;
}
