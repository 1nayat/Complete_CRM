using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AskKhadim.HRMS.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace AskKhadim.HRMS.Infrastructure.Models;

[Index("is_active", Name = "IX_departments_active")]
[Index("department_code", Name = "IX_departments_code")]
[Index("department_head_id", Name = "IX_departments_head")]
[Index("department_name", Name = "IX_departments_name")]
[Index("parent_department_id", Name = "IX_departments_parent")]
[Index("department_code", Name = "UQ_departments_code", IsUnique = true)]
public partial class department : OrgScopedEntity
{
    [Key]
    public Guid department_id { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string department_code { get; set; } = null!;

    [StringLength(150)]
    [Unicode(false)]
    public string department_name { get; set; } = null!;

    [StringLength(500)]
    [Unicode(false)]
    public string? description { get; set; }

    public Guid? parent_department_id { get; set; }

    public long? department_head_id { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? cost_center { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string? location { get; set; }

    public bool is_active { get; set; }

    [Precision(3)]
    public DateTime created_at { get; set; }

    [Precision(3)]
    public DateTime? updated_at { get; set; }

    [InverseProperty("parent_department")]
    public virtual ICollection<department> Inverseparent_department { get; set; } = new List<department>();

    [ForeignKey("department_head_id")]
    [InverseProperty("departments")]
    public virtual core_user? department_head { get; set; }

    [ForeignKey("parent_department_id")]
    [InverseProperty("Inverseparent_department")]
    public virtual department? parent_department { get; set; }

    [InverseProperty("department")]
    public virtual ICollection<user_employment_history> user_employment_histories { get; set; } = new List<user_employment_history>();

    [InverseProperty("department")]
    public virtual ICollection<user_hr> user_hrs { get; set; } = new List<user_hr>();
}
