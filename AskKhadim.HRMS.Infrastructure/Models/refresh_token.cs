using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AskKhadim.HRMS.Infrastructure.Models;

public partial class refresh_token
{
    [Key]
    public long id { get; set; }

    public long user_id { get; set; }

    [Precision(3)]
    public DateTime created_at { get; set; }

    [Precision(3)]
    public DateTime expires_at { get; set; }

    public bool revoked { get; set; }

    [Precision(3)]
    public DateTime? revoked_at { get; set; }

    [StringLength(45)]
    [Unicode(false)]
    public string? created_by_ip { get; set; }

    [MaxLength(64)]
    public byte[]? token_hash { get; set; }

    [MaxLength(64)]
    public byte[]? replaced_by_token_hash { get; set; }

    [StringLength(100)]
    public string? revoked_reason { get; set; }

    [ForeignKey("user_id")]
    [InverseProperty("refresh_tokens")]
    public virtual core_user user { get; set; } = null!;
}
