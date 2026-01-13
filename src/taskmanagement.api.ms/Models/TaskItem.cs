using System.ComponentModel.DataAnnotations;
using taskmanagement.api.ms.Models.Enums;

namespace taskmanagement.api.ms.Models
{
    public class TaskItem
    {
        public int Id { get; set; }

        [MaxLength(100)]
        public string Title { get; set; } = null!;

        [MaxLength(500)]
        public string? Description { get; set; }

        public TasksStatus Status { get; set; } = TasksStatus.Pending;

        public TaskPriority Priority { get; set; } = TaskPriority.Medium;

        public DateTime? DueDate { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
