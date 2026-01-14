using taskmanagement.api.ms.domain.DTOs;
using taskmanagement.api.ms.domain.Interface;
using taskmanagement.api.ms.DTOs;
using taskmanagement.api.ms.Interfaces;
using taskmanagement.api.ms.Models;
using taskmanagement.api.ms.Models.Enums;

namespace taskmanagement.api.ms.Services
{
    public class TaskManagementService: ITaskManagementService
    {

        private readonly ITaskService _taskService;

        public TaskManagementService(ITaskService taskService)
        {
            _taskService = taskService;
        }
        public async Task<CreateTaskDto> CreateTaskAsync(CreateTaskDto dto)
        {
            if(dto == null)
            {
                throw new ArgumentNullException(nameof(CreateTaskDto));
            }
            return await _taskService.AddTask(dto);
        }

        public async Task<UpdateTaskDto> UpdateTaskAsync(int id, UpdateTaskDto dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(UpdateTaskDto));
            }
            return await _taskService.UpdateTask(id , dto);
        }
        public async Task<TaskResultDto> GetTasksAsync(int page = 1,
            int pageSize = 10,
            TasksStatus? status = null,
            TaskPriority? priority = null,
            string? search = null)
        {
            if (pageSize == 0 && page == 0)
            {
                throw new Exception("Pagination Parameter must not be equal to 0!");
            }
            return await _taskService.GetTasks(page, pageSize,status,priority,search);
        }
        public async Task<TaskItem> GetTaskByIdAsync(int id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }
            return await _taskService.GetTaskById(id);
        }
        public async Task<TaskItem> RemoveTaskAsync(int id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }
            return await _taskService.RemoveTask(id);
        }
    }
}
