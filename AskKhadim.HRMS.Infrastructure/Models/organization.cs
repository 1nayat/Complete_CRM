using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AskKhadim.HRMS.Infrastructure.Models;

public partial class organization
{
    [Key]
    public Guid organization_id { get; set; }

    [StringLength(250)]
    public string name { get; set; } = null!;

    [StringLength(100)]
    public string? organization_type { get; set; }

    [StringLength(150)]
    public string? industry { get; set; }

    [StringLength(100)]
    public string? tax_registration_number { get; set; }

    public int? year_established { get; set; }

    [StringLength(50)]
    public string? company_size { get; set; }

    [StringLength(500)]
    public string? website_url { get; set; }

    public string? brief_description { get; set; }

    public string? primary_products { get; set; }

    [StringLength(500)]
    public string? target_market { get; set; }

    [StringLength(100)]
    public string? revenue_range { get; set; }

    [StringLength(100)]
    public string? preferred_plan { get; set; }

    public int? expected_user_count { get; set; }

    [StringLength(50)]
    public string? preferred_language { get; set; }

    [StringLength(100)]
    public string? time_zone { get; set; }

    public bool is_active { get; set; }

    [Precision(3)]
    public DateTime created_at { get; set; }

    public long? created_by { get; set; }

    [InverseProperty("organization")]
    public virtual ICollection<core_user> core_users { get; set; } = new List<core_user>();

    [InverseProperty("organization")]
    public virtual ICollection<invitation> invitations { get; set; } = new List<invitation>();

    [InverseProperty("organization")]
    public virtual ICollection<organization_address> organization_addresses { get; set; } = new List<organization_address>();

    [InverseProperty("organization")]
    public virtual ICollection<organization_contact> organization_contacts { get; set; } = new List<organization_contact>();

    [InverseProperty("organization")]
    public virtual ICollection<organization_file> organization_files { get; set; } = new List<organization_file>();

    [InverseProperty("organization")]
    public virtual ICollection<user_role> user_roles { get; set; } = new List<user_role>();
}
