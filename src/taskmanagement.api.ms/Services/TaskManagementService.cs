using taskmanagement.api.ms.domain.Interface;
using taskmanagement.api.ms.DTOs;
using taskmanagement.api.ms.Interfaces;

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
    }
}
