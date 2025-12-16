using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

namespace AskKhadim.HRMS.Infrastructure.Models;

[Table("user_profile")]
[Index("current_city", Name = "IX_user_profile_city")]
[Index("geo_point", Name = "IX_user_profile_geo_point")]
[Index("phone", Name = "IX_user_profile_phone")]
public partial class user_profile
{
    [Key]
    public long user_id { get; set; }

    public Guid? user_uuid { get; set; }

    [StringLength(60)]
    [Unicode(false)]
    public string first_name { get; set; } = null!;

    [StringLength(60)]
    [Unicode(false)]
    public string? middle_name { get; set; }

    [StringLength(60)]
    [Unicode(false)]
    public string last_name { get; set; } = null!;

    [StringLength(150)]
    [Unicode(false)]
    public string? personal_email { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? phone { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? alternate_phone { get; set; }

    public DateOnly? date_of_birth { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? gender { get; set; }

    [StringLength(5)]
    [Unicode(false)]
    public string? blood_group { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? marital_status { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? nationality { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string? father_name { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string? mother_name { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string? spouse_name { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string? current_address_line1 { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string? current_address_line2 { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? current_city { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? current_district { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? current_state { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? current_country { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? current_pincode { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string? permanent_address_line1 { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string? permanent_address_line2 { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? permanent_city { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? permanent_district { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? permanent_state { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? permanent_country { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? permanent_pincode { get; set; }

    public bool is_same_address { get; set; }

    public Geometry? geo_point { get; set; }

    [Precision(3)]
    public DateTime? geo_last_updated { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string? emergency_contact_name { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? emergency_contact_relationship { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? emergency_contact_phone { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? emergency_contact_alternate_phone { get; set; }

    [StringLength(300)]
    [Unicode(false)]
    public string? emergency_contact_address { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string? profile_photo_url { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string? resume_url { get; set; }

    [Precision(3)]
    public DateTime created_at { get; set; }

    [Precision(3)]
    public DateTime? updated_at { get; set; }

    [ForeignKey("user_id")]
    [InverseProperty("user_profile")]
    public virtual core_user user { get; set; } = null!;
}
