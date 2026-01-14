using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;
using taskmanagement.api.ms.domain.Database;
using taskmanagement.api.ms.domain.Service;
using taskmanagement.api.ms.DTOs;
using taskmanagement.api.ms.Interfaces;
using taskmanagement.api.ms.Models.Enums;

namespace taskmanagement.api.ms.Controllers
{
    [ApiController]
    [Route("task-management")]
    public class TasksController : ControllerBase
    {
        private readonly ITaskManagementService _taskManagementService;
        private readonly ITaskExportService _taskExportService;
        public TasksController(ITaskManagementService taskManagementService, ITaskExportService taskExportService)
        {
            _taskManagementService = taskManagementService;
            _taskExportService = taskExportService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTasks(int page = 1,int pageSize = 10,TasksStatus? status = null,
            TaskPriority? priority = null,string? search = null)
        {
            var task = await _taskManagementService.GetTasksAsync(page, pageSize, status, priority, search);
            return Ok(task);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask(CreateTaskDto dto)
        {
            var task =  await _taskManagementService.CreateTaskAsync(dto);
            return Ok(task);
        }
        [HttpGet("export")]
        public async Task<IActionResult> ExportTasks()
        {
            var csv = await _taskExportService.ExportTasksToCsvAsync();
            var bytes = Encoding.UTF8.GetBytes(csv);
            return File(bytes, "text/csv", "tasks.csv");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTaskById(int id)
        {
            var task = await _taskManagementService.GetTaskByIdAsync(id);
            return task == null ? NotFound() : Ok(task);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, UpdateTaskDto dto)
        {
            var task = await _taskManagementService.UpdateTaskAsync(id, dto);
            return Ok(task);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
           var task = await _taskManagementService.RemoveTaskAsync(id);
           return Ok(task);
        }
    }

}
