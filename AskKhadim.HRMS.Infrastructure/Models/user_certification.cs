using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AskKhadim.HRMS.Infrastructure.Models;

[Index("expiry_date", Name = "IX_user_certifications_expiry")]
[Index("user_id", Name = "IX_user_certifications_user")]
public partial class user_certification
{
    [Key]
    public Guid certification_id { get; set; }

    public long user_id { get; set; }

    [StringLength(250)]
    [Unicode(false)]
    public string certification_name { get; set; } = null!;

    [StringLength(200)]
    [Unicode(false)]
    public string? issuing_organization { get; set; }

    public DateOnly? issue_date { get; set; }

    public DateOnly? expiry_date { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string? credential_id { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string? credential_url { get; set; }

    public bool is_active { get; set; }

    [Precision(3)]
    public DateTime created_at { get; set; }

    [ForeignKey("user_id")]
    [InverseProperty("user_certifications")]
    public virtual core_user user { get; set; } = null!;
}
