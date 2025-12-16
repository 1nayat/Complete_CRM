using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AskKhadim.HRMS.Infrastructure.Models;

[Index("language", Name = "IX_user_languages_language")]
[Index("user_id", Name = "IX_user_languages_user")]
public partial class user_language
{
    [Key]
    public Guid language_id { get; set; }

    public long user_id { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string language { get; set; } = null!;

    [StringLength(20)]
    [Unicode(false)]
    public string proficiency { get; set; } = null!;

    public bool is_primary { get; set; }

    [Precision(3)]
    public DateTime created_at { get; set; }

    [ForeignKey("user_id")]
    [InverseProperty("user_languages")]
    public virtual core_user user { get; set; } = null!;
}
