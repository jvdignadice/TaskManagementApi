using taskmanagement.api.ms.domain.DTOs;
using taskmanagement.api.ms.DTOs;
using taskmanagement.api.ms.Models;
using taskmanagement.api.ms.Models.Enums;

namespace taskmanagement.api.ms.domain.Interface
{
    public interface ITaskService
    {
        Task<CreateTaskDto> AddTask(CreateTaskDto dto);
        Task<TaskItem> GetTaskById(int id);
        Task<TaskResultDto> GetTasks(int page = 1,
            int pageSize = 10,
            TasksStatus? status = null,
            TaskPriority? priority = null,
            string? search = null);
        Task<TaskItem> RemoveTask(int id);
        Task<UpdateTaskDto> UpdateTask(int id, UpdateTaskDto dto);
    }
}
