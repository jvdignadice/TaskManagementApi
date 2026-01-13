using taskmanagement.api.ms.domain.Interface;
using taskmanagement.api.ms.DTOs;
using taskmanagement.api.ms.Models;
using Microsoft.EntityFrameworkCore;
using taskmanagement.api.ms.domain.Database;

namespace taskmanagement.api.ms.domain.Service
{
    public class TaskService: ITaskService
    {
        private readonly TaskManagementAppDbContext _context;

        public TaskService(TaskManagementAppDbContext context)
        {
            _context = context;
        }

        public async Task<CreateTaskDto> AddTask(CreateTaskDto dto)
        {
            var task = new TaskItem
            {
                Title = dto.Title,
                Description = dto.Description,
                Priority = dto.Priority,
                DueDate = dto.DueDate
            };

            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();

            return dto;
        }
    }
}
