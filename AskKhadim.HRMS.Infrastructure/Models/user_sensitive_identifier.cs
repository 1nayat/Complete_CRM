using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AskKhadim.HRMS.Infrastructure.Models;

public partial class user_sensitive_identifier
{
    [Key]
    public long user_id { get; set; }

    [MaxLength(64)]
    public byte[]? pan_hash { get; set; }

    [MaxLength(64)]
    public byte[]? aadhaar_hash { get; set; }

    [MaxLength(1024)]
    public byte[]? pan_encrypted { get; set; }

    [MaxLength(2048)]
    public byte[]? aadhaar_encrypted { get; set; }

    [MaxLength(1024)]
    public byte[]? passport_encrypted { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? govt_id_type { get; set; }

    [MaxLength(2048)]
    public byte[]? govt_id_number_encrypted { get; set; }

    public DateOnly? govt_id_issue_date { get; set; }

    public DateOnly? govt_id_expiry_date { get; set; }

    [Precision(3)]
    public DateTime created_at { get; set; }

    [Precision(3)]
    public DateTime? updated_at { get; set; }

    [ForeignKey("user_id")]
    [InverseProperty("user_sensitive_identifier")]
    public virtual core_user user { get; set; } = null!;
}
