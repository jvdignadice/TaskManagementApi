using taskmanagement.api.ms.Models;

namespace taskmanagement.api.ms.domain.DTOs
{
    public class TaskResultDto
    {
        public int Total { get; set; }
        public List<TaskItem> TaskItems { get; set; }
    }
}
