using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AskKhadim.HRMS.Infrastructure.Models;

[Table("user_health_insurance")]
[Index("is_active", Name = "IX_user_health_insurance_active")]
[Index("user_id", Name = "IX_user_health_insurance_user")]
public partial class user_health_insurance
{
    [Key]
    public Guid health_insurance_id { get; set; }

    public long user_id { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string? provider_name { get; set; }

    [MaxLength(1024)]
    public byte[]? policy_number_encrypted { get; set; }

    public string? coverage_details { get; set; }

    public DateOnly? start_date { get; set; }

    public DateOnly? end_date { get; set; }

    public bool is_active { get; set; }

    [Precision(3)]
    public DateTime created_at { get; set; }

    [Precision(3)]
    public DateTime? updated_at { get; set; }

    [ForeignKey("user_id")]
    [InverseProperty("user_health_insurances")]
    public virtual core_user user { get; set; } = null!;
}
