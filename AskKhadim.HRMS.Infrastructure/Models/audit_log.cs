using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AskKhadim.HRMS.Infrastructure.Models;

public partial class audit_log
{
    [Key]
    public long audit_id { get; set; }

    public long? actor_user_id { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? actor_role { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string action_type { get; set; } = null!;

    [StringLength(100)]
    [Unicode(false)]
    public string? entity_type { get; set; }

    [StringLength(100)]
    public string? entity_id { get; set; }

    public string? old_value { get; set; }

    public string? new_value { get; set; }

    [Precision(3)]
    public DateTime timestamp { get; set; }

    public Guid? correlation_id { get; set; }
}
