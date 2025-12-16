using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AskKhadim.HRMS.Infrastructure.Models;

[Index(nameof(organization_id), nameof(email), IsUnique = true)]
public partial class organization_invitation
{
    [Key]
    public Guid invitation_id { get; set; }

    public Guid organization_id { get; set; }

    [StringLength(150)]
    public string email { get; set; } = null!;

    public Guid role_id { get; set; }

    [StringLength(100)]
    public string? designation { get; set; }

    public byte[] invite_token_hash { get; set; } = null!;

    [Precision(3)]
    public DateTime expires_at { get; set; }

    [Precision(3)]
    public DateTime? accepted_at { get; set; }

    public long invited_by { get; set; }

    [Precision(3)]
    public DateTime created_at { get; set; }

    // 🔗 Navigation properties
    [ForeignKey(nameof(organization_id))]
    public virtual organization organization { get; set; } = null!;

    [ForeignKey(nameof(role_id))]
    public virtual role role { get; set; } = null!;

    [ForeignKey(nameof(invited_by))]
    public virtual core_user invited_by_user { get; set; } = null!;
}
