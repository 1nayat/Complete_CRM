using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AskKhadim.HRMS.Infrastructure.Models;

[Index("expiry_date", Name = "IX_user_documents_expiry")]
[Index("document_type", Name = "IX_user_documents_type")]
[Index("user_id", Name = "IX_user_documents_user")]
[Index("is_verified", Name = "IX_user_documents_verified")]
public partial class user_document
{
    [Key]
    public Guid document_id { get; set; }

    public long user_id { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string document_type { get; set; } = null!;

    [StringLength(255)]
    [Unicode(false)]
    public string document_name { get; set; } = null!;

    [StringLength(500)]
    [Unicode(false)]
    public string document_url { get; set; } = null!;

    public int? file_size_kb { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? mime_type { get; set; }

    [Precision(3)]
    public DateTime upload_date { get; set; }

    public long? uploaded_by { get; set; }

    public DateOnly? expiry_date { get; set; }

    public bool is_verified { get; set; }

    public long? verified_by { get; set; }

    [Precision(3)]
    public DateTime? verified_at { get; set; }

    [ForeignKey("uploaded_by")]
    [InverseProperty("user_documentuploaded_byNavigations")]
    public virtual core_user? uploaded_byNavigation { get; set; }

    [ForeignKey("user_id")]
    [InverseProperty("user_documentusers")]
    public virtual core_user user { get; set; } = null!;

    [ForeignKey("verified_by")]
    [InverseProperty("user_documentverified_byNavigations")]
    public virtual core_user? verified_byNavigation { get; set; }
}
