using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AskKhadim.HRMS.Infrastructure.Models;

[Table("user_appraisal_history")]
[Index("appraisal_period_start", "appraisal_period_end", Name = "IX_user_appraisal_history_period")]
[Index("user_id", Name = "IX_user_appraisal_history_user")]
public partial class user_appraisal_history
{
    [Key]
    public Guid appraisal_id { get; set; }

    public long user_id { get; set; }

    public DateOnly appraisal_period_start { get; set; }

    public DateOnly appraisal_period_end { get; set; }

    [Column(TypeName = "decimal(5, 2)")]
    public decimal? overall_rating { get; set; }

    public string? summary { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string? appraisal_document_url { get; set; }

    public long? appraised_by { get; set; }

    [Precision(3)]
    public DateTime created_at { get; set; }

    [ForeignKey("appraised_by")]
    [InverseProperty("user_appraisal_historyappraised_byNavigations")]
    public virtual core_user? appraised_byNavigation { get; set; }

    [ForeignKey("user_id")]
    [InverseProperty("user_appraisal_historyusers")]
    public virtual core_user user { get; set; } = null!;
}
