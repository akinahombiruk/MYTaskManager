using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MYTaskManager.Models;

[Index("Email", Name = "UQ__Users__A9D1053458FABC97", IsUnique = true)]
public partial class User
{
    [Key]
    public int UserId { get; set; }

    [StringLength(100)]
    public string FullName { get; set; } = null!;

    [StringLength(100)]
    public string Email { get; set; } = null!;

    [StringLength(255)]
    public string PasswordHash { get; set; } = null!;

    [StringLength(50)]
    public string Role { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [InverseProperty("AssignedToNavigation")]
    public virtual ICollection<Task> TaskAssignedToNavigations { get; set; } = new List<Task>();

    [InverseProperty("UploadedByNavigation")]
    public virtual ICollection<TaskAttachment> TaskAttachments { get; set; } = new List<TaskAttachment>();

    [InverseProperty("CreatedByNavigation")]
    public virtual ICollection<Task> TaskCreatedByNavigations { get; set; } = new List<Task>();
}
