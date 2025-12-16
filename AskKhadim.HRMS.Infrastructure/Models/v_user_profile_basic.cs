using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AskKhadim.HRMS.Infrastructure.Models;

[Keyless]
public partial class v_user_profile_basic
{
    public long user_id { get; set; }

    public Guid user_uuid { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string employee_id { get; set; } = null!;

    [StringLength(150)]
    [Unicode(false)]
    public string email { get; set; } = null!;

    [StringLength(60)]
    [Unicode(false)]
    public string? first_name { get; set; }

    [StringLength(60)]
    [Unicode(false)]
    public string? last_name { get; set; }

    [StringLength(150)]
    [Unicode(false)]
    public string? designation { get; set; }

    [StringLength(150)]
    [Unicode(false)]
    public string? department_name { get; set; }

    public bool is_active { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string? linkedin_profile_url { get; set; }

    public DateOnly? joining_date { get; set; }
}
