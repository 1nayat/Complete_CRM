using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AskKhadim.HRMS.Infrastructure.Models;

public partial class user_role
{
    [Key]
    public long user_role_id { get; set; }

    public long user_id { get; set; }

    public Guid role_id { get; set; }

    public Guid? organization_id { get; set; }

    [Precision(3)]
    public DateTime assigned_at { get; set; }

    public long? assigned_by { get; set; }

    [ForeignKey("organization_id")]
    [InverseProperty("user_roles")]
    public virtual organization? organization { get; set; }

    [ForeignKey("role_id")]
    [InverseProperty("user_roles")]
    public virtual role role { get; set; } = null!;

    [ForeignKey("user_id")]
    [InverseProperty("user_roles")]
    public virtual core_user user { get; set; } = null!;
}
