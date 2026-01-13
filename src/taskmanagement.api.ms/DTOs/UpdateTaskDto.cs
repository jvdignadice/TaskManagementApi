using taskmanagement.api.ms.Models.Enums;

namespace taskmanagement.api.ms.DTOs
{
    public class UpdateTaskDto : CreateTaskDto
    {
        public TasksStatus Status { get; set; }
    }
}
