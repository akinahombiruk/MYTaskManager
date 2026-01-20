using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MYTaskManager.Models;

public partial class Task
{
    [Key]
    public int TaskId { get; set; }

    public int? ProjectId { get; set; }

    public int? AssignedTo { get; set; }

    public int? CreatedBy { get; set; }

    [StringLength(150)]
    public string Title { get; set; } = null!;

    [StringLength(1000)]
    public string? Description { get; set; }

    public int? StatusId { get; set; }

    public int? PriorityId { get; set; }

    public DateOnly? DueDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [ForeignKey("AssignedTo")]
    [InverseProperty("TaskAssignedToNavigations")]
    public virtual User? AssignedToNavigation { get; set; }

    [ForeignKey("CreatedBy")]
    [InverseProperty("TaskCreatedByNavigations")]
    public virtual User? CreatedByNavigation { get; set; }

    [ForeignKey("PriorityId")]
    [InverseProperty("Tasks")]
    public virtual TaskPriority? Priority { get; set; }

    [ForeignKey("ProjectId")]
    [InverseProperty("Tasks")]
    public virtual Project? Project { get; set; }

    [ForeignKey("StatusId")]
    [InverseProperty("Tasks")]
    public virtual TaskStatus? Status { get; set; }

    [InverseProperty("Task")]
    public virtual ICollection<TaskAttachment> TaskAttachments { get; set; } = new List<TaskAttachment>();
}
