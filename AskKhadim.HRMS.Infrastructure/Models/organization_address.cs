using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AskKhadim.HRMS.Infrastructure.Models;

public partial class organization_address
{
    [Key]
    public long address_id { get; set; }

    public Guid organization_id { get; set; }

    [StringLength(500)]
    public string? address_line1 { get; set; }

    [StringLength(500)]
    public string? address_line2 { get; set; }

    [StringLength(150)]
    public string? city { get; set; }

    [StringLength(150)]
    public string? state_province { get; set; }

    [StringLength(50)]
    public string? postal_code { get; set; }

    [StringLength(150)]
    public string? country { get; set; }

    [Precision(3)]
    public DateTime created_at { get; set; }

    [ForeignKey("organization_id")]
    [InverseProperty("organization_addresses")]
    public virtual organization organization { get; set; } = null!;
}
