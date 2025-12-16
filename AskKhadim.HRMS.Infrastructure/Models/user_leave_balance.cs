using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AskKhadim.HRMS.Infrastructure.Models;

[Table("user_leave_balance")]
[Index("user_id", Name = "IX_user_leave_balance_user")]
[Index("user_id", "leave_type", "year", Name = "UQ_user_leave_balance_user_type_year", IsUnique = true)]
public partial class user_leave_balance
{
    [Key]
    public Guid leave_balance_id { get; set; }

    public long user_id { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string leave_type { get; set; } = null!;

    [Column(TypeName = "decimal(6, 2)")]
    public decimal balance { get; set; }

    public short year { get; set; }

    [Precision(3)]
    public DateTime updated_at { get; set; }

    [ForeignKey("user_id")]
    [InverseProperty("user_leave_balances")]
    public virtual core_user user { get; set; } = null!;
}
