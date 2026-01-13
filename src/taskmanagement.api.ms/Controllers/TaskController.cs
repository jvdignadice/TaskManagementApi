using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using taskmanagement.api.ms.DTOs;
using taskmanagement.api.ms.Interfaces;
using taskmanagement.api.ms.Models;
using taskmanagement.api.ms.Models.Enums;
using taskmanagement.api.ms.domain.Database;

namespace taskmanagement.api.ms.Controllers
{
    [ApiController]
    [Route("task-management")]
    public class TasksController : ControllerBase
    {
        private readonly ITaskManagementService _taskManagementService;
        private readonly TaskManagementAppDbContext _context;
        public TasksController(ITaskManagementService taskManagementService, TaskManagementAppDbContext context)
        {
            _taskManagementService = taskManagementService;
            _context = context;
        }

        [HttpGet("getTaskAsyncWithPagination")]
        public async Task<IActionResult> GetTasks(
            int page = 1,
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

            return Ok(new { total, tasks });
        }

        [HttpPost("addTaskAsync")]
        public async Task<IActionResult> CreateTask(CreateTaskDto dto)
        {
            var task =  await _taskManagementService.CreateTaskAsync(dto);
            return Ok(task);
        }

        [HttpGet("{id}", Name = "GetTaskByIdAsync")]
        public async Task<IActionResult> GetTaskById(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            return task == null ? NotFound() : Ok(task);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, UpdateTaskDto dto)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null) return NotFound();

            task.Title = dto.Title;
            task.Description = dto.Description;
            task.Priority = dto.Priority;
            task.Status = dto.Status;
            task.DueDate = dto.DueDate;
            task.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}", Name = "RemoveTaskAsync")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null) return NotFound();

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }

}
