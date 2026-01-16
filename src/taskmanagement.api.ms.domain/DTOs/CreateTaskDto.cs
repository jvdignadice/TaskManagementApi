using taskmanagement.api.ms.Models.Enums;

namespace taskmanagement.api.ms.DTOs
{
    public class CreateTaskDto
    {
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public int Priority { get; set; }
        public DateTime? DueDate { get; set; }
    }
}
