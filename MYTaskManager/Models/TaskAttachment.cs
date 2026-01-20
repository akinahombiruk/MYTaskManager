using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MYTaskManager.Models;

public partial class TaskAttachment
{
    [Key]
    public int AttachmentId { get; set; }

    public int? TaskId { get; set; }

    [StringLength(255)]
    public string FileName { get; set; } = null!;

    [StringLength(500)]
    public string FilePath { get; set; } = null!;

    public int? UploadedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UploadedAt { get; set; }

    [ForeignKey("TaskId")]
    [InverseProperty("TaskAttachments")]
    public virtual Task? Task { get; set; }

    [ForeignKey("UploadedBy")]
    [InverseProperty("TaskAttachments")]
    public virtual User? UploadedByNavigation { get; set; }
}
