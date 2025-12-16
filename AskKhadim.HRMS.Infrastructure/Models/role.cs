using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AskKhadim.HRMS.Infrastructure.Models;

[Index("role_name", Name = "UQ__roles__783254B1FAA3D0AF", IsUnique = true)]
public partial class role
{
    [Key]
    public Guid role_id { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string role_name { get; set; } = null!;

    [StringLength(500)]
    public string? description { get; set; }

    [Precision(3)]
    public DateTime created_at { get; set; }

    [InverseProperty("role")]
    public virtual ICollection<user_role> user_roles { get; set; } = new List<user_role>();
}
