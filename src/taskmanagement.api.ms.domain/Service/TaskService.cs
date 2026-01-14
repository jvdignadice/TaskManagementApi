using Microsoft.EntityFrameworkCore;
using taskmanagement.api.ms.domain.Database;
using taskmanagement.api.ms.domain.DTOs;
using taskmanagement.api.ms.domain.Interface;
using taskmanagement.api.ms.DTOs;
using taskmanagement.api.ms.Models;
using taskmanagement.api.ms.Models.Enums;

namespace taskmanagement.api.ms.domain.Service
{
    public class TaskService: ITaskService
    {
        private readonly ITaskDbContext _context;

        public TaskService(ITaskDbContext context)
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
        public async Task<UpdateTaskDto> UpdateTask(int id, UpdateTaskDto dto)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null) throw new ArgumentNullException(nameof(task));

            task.Title = dto.Title;
            task.Description = dto.Description;
            task.Priority = dto.Priority;
            task.Status = dto.Status;
            task.DueDate = dto.DueDate;
            task.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return dto;
        }

        public async Task<TaskItem> RemoveTask(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null) throw new ArgumentNullException(nameof(task));

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();

            return task;
        }

        public async Task<TaskItem> GetTaskById(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            return task == null ? new TaskItem() : task;
        }
        public async Task<TaskResultDto> GetTasks(int page = 1,
            int pageSize = 10,
            TasksStatus? status = null,
            TaskPriority? priority = null,
            string? search = null)
        {
            var query = _context.Tasks.AsQueryable();

            if (status.HasValue)
                query = query.Where(t => t.Status == status);

            if (priority.HasValue)
                query = query.Where(t => t.Priority == priority);

            if (!string.IsNullOrWhiteSpace(search))
                query = query.Where(t =>
                    t.Title.Contains(search) ||
                    (t.Description != null && t.Description.Contains(search)));

            var total = await query.CountAsync();

            var tasks = await query
                .OrderByDescending(t => t.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            var results = new TaskResultDto
            {
                Total = total,
                TaskItems = tasks
            };
            return results;
        }
    }
}
