using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AskKhadim.HRMS.Infrastructure.Models;

[Table("user_salary")]
[Index("is_current", Name = "IX_user_salary_current")]
[Index("user_id", Name = "IX_user_salary_user")]
public partial class user_salary
{
    [Key]
    public Guid salary_id { get; set; }

    public long user_id { get; set; }

    [MaxLength(2048)]
    public byte[] salary_encrypted { get; set; } = null!;

    [StringLength(3)]
    [Unicode(false)]
    public string currency { get; set; } = null!;

    public DateOnly effective_from { get; set; }

    public DateOnly? effective_to { get; set; }

    public bool is_current { get; set; }

    [Precision(3)]
    public DateTime created_at { get; set; }

    public long? created_by { get; set; }

    [ForeignKey("user_id")]
    [InverseProperty("user_salaries")]
    public virtual core_user user { get; set; } = null!;
}
