using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

namespace AskKhadim.HRMS.Infrastructure.Models;

[Table("user_attendance")]
[Index("location", Name = "IX_user_attendance_location")]
[Index("user_id", "work_date", Name = "IX_user_attendance_user_date")]
public partial class user_attendance
{
    [Key]
    public Guid attendance_id { get; set; }

    public long user_id { get; set; }

    public DateOnly work_date { get; set; }

    [Precision(3)]
    public DateTime? punch_in { get; set; }

    [Precision(3)]
    public DateTime? punch_out { get; set; }

    public int? duration_minutes { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string status { get; set; } = null!;

    [StringLength(255)]
    [Unicode(false)]
    public string? device_info { get; set; }

    public Geometry? location { get; set; }

    [Precision(3)]
    public DateTime created_at { get; set; }

    [ForeignKey("user_id")]
    [InverseProperty("user_attendances")]
    public virtual core_user user { get; set; } = null!;
}
