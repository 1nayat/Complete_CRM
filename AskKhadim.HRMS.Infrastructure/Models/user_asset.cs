using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AskKhadim.HRMS.Infrastructure.Models;

[Index("status", Name = "IX_user_assets_status")]
[Index("asset_type", Name = "IX_user_assets_type")]
[Index("user_id", Name = "IX_user_assets_user")]
public partial class user_asset
{
    [Key]
    public Guid asset_id { get; set; }

    public long user_id { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string asset_type { get; set; } = null!;

    [StringLength(200)]
    [Unicode(false)]
    public string? asset_name { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? brand { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? model { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string? serial_number { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? asset_tag { get; set; }

    public DateOnly? purchase_date { get; set; }

    public DateOnly? warranty_expiry_date { get; set; }

    public DateOnly? assigned_date { get; set; }

    public DateOnly? return_date { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? condition_on_assignment { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? condition_on_return { get; set; }

    [Column(TypeName = "decimal(12, 2)")]
    public decimal? estimated_value { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string status { get; set; } = null!;

    public string? notes { get; set; }

    public long? assigned_by { get; set; }

    public long? returned_to { get; set; }

    [Precision(3)]
    public DateTime created_at { get; set; }

    [ForeignKey("user_id")]
    [InverseProperty("user_assets")]
    public virtual core_user user { get; set; } = null!;
}
