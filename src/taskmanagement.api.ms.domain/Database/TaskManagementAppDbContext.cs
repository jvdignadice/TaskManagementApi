using Microsoft.EntityFrameworkCore;
using taskmanagement.api.ms.domain.Interface;
using taskmanagement.api.ms.Models;

namespace taskmanagement.api.ms.domain.Database
{
    public class TaskManagementAppDbContext : DbContext, ITaskDbContext
    {
        public TaskManagementAppDbContext(DbContextOptions<TaskManagementAppDbContext> options)
           : base(options) { }

        public DbSet<TaskItem> Tasks { get; set; }

    }
}
