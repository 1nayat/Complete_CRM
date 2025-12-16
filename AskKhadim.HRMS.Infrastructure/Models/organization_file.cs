using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AskKhadim.HRMS.Infrastructure.Models;

public partial class organization_file
{
    [Key]
    public Guid file_id { get; set; }

    public Guid organization_id { get; set; }

    [StringLength(500)]
    public string file_name { get; set; } = null!;

    [StringLength(1000)]
    public string file_url { get; set; } = null!;

    [StringLength(100)]
    public string? content_type { get; set; }

    public long? size_bytes { get; set; }

    public long? uploaded_by { get; set; }

    [Precision(3)]
    public DateTime uploaded_at { get; set; }

    [ForeignKey("organization_id")]
    [InverseProperty("organization_files")]
    public virtual organization organization { get; set; } = null!;
}
