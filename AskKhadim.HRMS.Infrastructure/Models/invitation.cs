using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AskKhadim.HRMS.Infrastructure.Models;

[Index("email", Name = "IX_invitations_email")]
public partial class invitation
{
    [Key]
    public Guid invitation_id { get; set; }

    [StringLength(150)]
    [Unicode(false)]
    public string email { get; set; } = null!;

    public Guid organization_id { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string role_name { get; set; } = null!;

    [StringLength(128)]
    [Unicode(false)]
    public string token_hash { get; set; } = null!;

    [Precision(3)]
    public DateTime expires_at { get; set; }

    [Precision(3)]
    public DateTime created_at { get; set; }

    public long? created_by { get; set; }

    public bool used { get; set; }

    [Precision(3)]
    public DateTime? used_at { get; set; }

    public long? used_by { get; set; }

    [ForeignKey("organization_id")]
    [InverseProperty("invitations")]
    public virtual organization organization { get; set; } = null!;
}
