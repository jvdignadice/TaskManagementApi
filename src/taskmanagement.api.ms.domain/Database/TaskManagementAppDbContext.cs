using Microsoft.EntityFrameworkCore;
using taskmanagement.api.ms.Models;

namespace taskmanagement.api.ms.domain.Database
{
    public class TaskManagementAppDbContext : DbContext
    {
        public DbSet<TaskItem> Tasks => Set<TaskItem>();

        public TaskManagementAppDbContext(DbContextOptions<TaskManagementAppDbContext> options)
              : base(options) { }
    }
}
