using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AskKhadim.HRMS.Infrastructure.Models;

[Index("completion_status", Name = "IX_user_training_records_status")]
[Index("user_id", Name = "IX_user_training_records_user")]
public partial class user_training_record
{
    [Key]
    public Guid training_id { get; set; }

    public long user_id { get; set; }

    [StringLength(300)]
    [Unicode(false)]
    public string training_name { get; set; } = null!;

    [StringLength(200)]
    [Unicode(false)]
    public string? provider { get; set; }

    public DateOnly? start_date { get; set; }

    public DateOnly? end_date { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string completion_status { get; set; } = null!;

    [Column(TypeName = "decimal(6, 2)")]
    public decimal? score { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string? certificate_url { get; set; }

    public string? notes { get; set; }

    [Precision(3)]
    public DateTime created_at { get; set; }

    [ForeignKey("user_id")]
    [InverseProperty("user_training_records")]
    public virtual core_user user { get; set; } = null!;
}
