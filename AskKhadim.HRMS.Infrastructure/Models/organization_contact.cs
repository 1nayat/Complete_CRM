using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AskKhadim.HRMS.Infrastructure.Models;

public partial class organization_contact
{
    [Key]
    public long contact_id { get; set; }

    public Guid organization_id { get; set; }

    [StringLength(250)]
    public string? full_name { get; set; }

    [StringLength(150)]
    public string? job_title { get; set; }

    [StringLength(150)]
    public string? email { get; set; }

    [StringLength(50)]
    public string? phone { get; set; }

    [StringLength(50)]
    public string? alt_phone { get; set; }

    public bool is_primary { get; set; }

    [Precision(3)]
    public DateTime created_at { get; set; }

    public long? created_by { get; set; }

    [ForeignKey("organization_id")]
    [InverseProperty("organization_contacts")]
    public virtual organization organization { get; set; } = null!;
}
