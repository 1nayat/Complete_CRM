using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AskKhadim.HRMS.Infrastructure.Models;

[Index("rating_period_start", "rating_period_end", Name = "IX_user_performance_ratings_period")]
[Index("user_id", Name = "IX_user_performance_ratings_user")]
public partial class user_performance_rating
{
    [Key]
    public Guid rating_id { get; set; }

    public long user_id { get; set; }

    public DateOnly rating_period_start { get; set; }

    public DateOnly rating_period_end { get; set; }

    [Column(TypeName = "decimal(5, 2)")]
    public decimal? rating_score { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? rating_level { get; set; }

    public string? comments { get; set; }

    public long? rated_by { get; set; }

    [Precision(3)]
    public DateTime created_at { get; set; }

    [ForeignKey("rated_by")]
    [InverseProperty("user_performance_ratingrated_byNavigations")]
    public virtual core_user? rated_byNavigation { get; set; }

    [ForeignKey("user_id")]
    [InverseProperty("user_performance_ratingusers")]
    public virtual core_user user { get; set; } = null!;
}
