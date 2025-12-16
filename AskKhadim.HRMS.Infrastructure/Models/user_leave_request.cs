using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AskKhadim.HRMS.Infrastructure.Models;

[Index("start_date", Name = "IX_user_leave_requests_start_date")]
[Index("status", Name = "IX_user_leave_requests_status")]
[Index("user_id", Name = "IX_user_leave_requests_user")]
public partial class user_leave_request
{
    [Key]
    public Guid leave_request_id { get; set; }

    public long user_id { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string leave_type { get; set; } = null!;

    public DateOnly start_date { get; set; }

    public DateOnly end_date { get; set; }

    [Column(TypeName = "decimal(6, 2)")]
    public decimal days { get; set; }

    [StringLength(1000)]
    [Unicode(false)]
    public string? reason { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string status { get; set; } = null!;

    [Precision(3)]
    public DateTime requested_at { get; set; }

    public long? processed_by { get; set; }

    [Precision(3)]
    public DateTime? processed_at { get; set; }

    [ForeignKey("processed_by")]
    [InverseProperty("user_leave_requestprocessed_byNavigations")]
    public virtual core_user? processed_byNavigation { get; set; }

    [ForeignKey("user_id")]
    [InverseProperty("user_leave_requestusers")]
    public virtual core_user user { get; set; } = null!;
}
