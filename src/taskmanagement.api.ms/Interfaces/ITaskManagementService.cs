using taskmanagement.api.ms.domain.DTOs;
using taskmanagement.api.ms.DTOs;
using taskmanagement.api.ms.Models;
using taskmanagement.api.ms.Models.Enums;

namespace taskmanagement.api.ms.Interfaces
{
    public interface ITaskManagementService
    {
        Task<CreateTaskDto> CreateTaskAsync(CreateTaskDto dto);
        Task<TaskItem> GetTaskByIdAsync(int id);
        Task<TaskResultDto> GetTasksAsync(int page = 1,
            int pageSize = 10,
            TasksStatus? status = null,
            TaskPriority? priority = null,
            string? search = null);
        Task<UpdateTaskDto> UpdateTaskAsync(int id, UpdateTaskDto dto);
        Task<TaskItem> RemoveTaskAsync(int id);
    }
}
