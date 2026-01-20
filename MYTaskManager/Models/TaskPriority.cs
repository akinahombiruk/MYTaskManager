using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MYTaskManager.Models;

[Table("TaskPriority")]
public partial class TaskPriority
{
    [Key]
    public int PriorityId { get; set; }

    [StringLength(50)]
    public string PriorityName { get; set; } = null!;

    [InverseProperty("Priority")]
    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();
}
