using taskmanagement.api.ms.DTOs;

namespace taskmanagement.api.ms.Interfaces
{
    public interface ITaskManagementService
    {
        Task<CreateTaskDto> CreateTaskAsync(CreateTaskDto dto);
    }
}
