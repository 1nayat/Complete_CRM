using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AskKhadim.HRMS.Infrastructure.Models;

public partial class legal_consent
{
    [Key]
    public long consent_id { get; set; }

    public Guid? organization_id { get; set; }

    public long? user_id { get; set; }

    [StringLength(200)]
    public string consent_key { get; set; } = null!;

    public string? consent_text { get; set; }

    [Precision(3)]
    public DateTime accepted_at { get; set; }

    [StringLength(45)]
    public string? accepted_by_ip { get; set; }
}
