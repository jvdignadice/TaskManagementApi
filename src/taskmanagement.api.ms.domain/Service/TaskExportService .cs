using Microsoft.EntityFrameworkCore;
using System.Text;
using taskmanagement.api.ms.domain.Interface;
using taskmanagement.api.ms.Models.Enums;

namespace taskmanagement.api.ms.domain.Service
{
    public interface ITaskExportService
    {
        Task<string> ExportTasksToCsvAsync(
            TasksStatus? status = null,
            TaskPriority? priority = null,
            string? search = null);
    }

    public class TaskExportService : ITaskExportService
    {
        private readonly ITaskDbContext _context;

        public TaskExportService(ITaskDbContext context)
        {
            _context = context;
        }

        public async Task<string> ExportTasksToCsvAsync(
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

            var tasks = await query
                .OrderByDescending(t => t.CreatedAt)
                .ToListAsync();

            // Create CSV
            var sb = new StringBuilder();
            sb.AppendLine("Id,Title,Description,Priority,Status,DueDate,CreatedAt,UpdatedAt");

            foreach (var task in tasks)
            {
                sb.AppendLine($"{task.Id}," +
                              $"\"{EscapeCsv(task.Title)}\"," +
                              $"\"{EscapeCsv(task.Description)}\"," +
                              $"{task.Priority}," +
                              $"{task.Status}," +
                              $"{task.DueDate:yyyy-MM-dd HH:mm:ss}," +
                              $"{task.CreatedAt:yyyy-MM-dd HH:mm:ss}," +
                              $"{task.UpdatedAt:yyyy-MM-dd HH:mm:ss}");
            }

            return sb.ToString();
        }

        private string EscapeCsv(string? value)
        {
            if (string.IsNullOrEmpty(value)) return "";
            var escaped = value.Replace("\"", "\"\"");
            return escaped;
        }
    }
}
