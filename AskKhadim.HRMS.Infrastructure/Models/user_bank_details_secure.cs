using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AskKhadim.HRMS.Infrastructure.Models;

[Table("user_bank_details_secure")]
[Index("is_primary", Name = "IX_user_bank_details_is_primary")]
[Index("user_id", Name = "IX_user_bank_details_user")]
public partial class user_bank_details_secure
{
    [Key]
    public Guid bank_detail_id { get; set; }

    public long user_id { get; set; }

    [StringLength(150)]
    [Unicode(false)]
    public string? bank_name { get; set; }

    [MaxLength(1024)]
    public byte[]? account_number_encrypted { get; set; }

    [MaxLength(512)]
    public byte[]? ifsc_code_encrypted { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string? branch_name { get; set; }

    public bool is_primary { get; set; }

    public bool is_salary_account { get; set; }

    [MaxLength(1024)]
    public byte[]? account_holder_name_encrypted { get; set; }

    public bool is_verified { get; set; }

    [Precision(3)]
    public DateTime? verified_at { get; set; }

    [Precision(3)]
    public DateTime created_at { get; set; }

    [Precision(3)]
    public DateTime? updated_at { get; set; }

    [ForeignKey("user_id")]
    [InverseProperty("user_bank_details_secures")]
    public virtual core_user user { get; set; } = null!;
}
