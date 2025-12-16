using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AskKhadim.HRMS.Infrastructure.Models;

[Table("user_employment_history")]
[Index("effective_from", "effective_to", Name = "IX_user_employment_history_effective_dates")]
[Index("user_id", Name = "IX_user_employment_history_user")]
public partial class user_employment_history
{
    [Key]
    public Guid history_id { get; set; }

    public long user_id { get; set; }

    public Guid? department_id { get; set; }

    [StringLength(150)]
    [Unicode(false)]
    public string? designation { get; set; }

    public long? reporting_manager_id { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? employment_type { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string? work_location { get; set; }

    public DateOnly effective_from { get; set; }

    public DateOnly? effective_to { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string? change_reason { get; set; }

    public long? changed_by { get; set; }

    [Precision(3)]
    public DateTime created_at { get; set; }

    [ForeignKey("changed_by")]
    [InverseProperty("user_employment_historychanged_byNavigations")]
    public virtual core_user? changed_byNavigation { get; set; }

    [ForeignKey("department_id")]
    [InverseProperty("user_employment_histories")]
    public virtual department? department { get; set; }

    [ForeignKey("reporting_manager_id")]
    [InverseProperty("user_employment_historyreporting_managers")]
    public virtual core_user? reporting_manager { get; set; }

    [ForeignKey("user_id")]
    [InverseProperty("user_employment_historyusers")]
    public virtual core_user user { get; set; } = null!;
}
