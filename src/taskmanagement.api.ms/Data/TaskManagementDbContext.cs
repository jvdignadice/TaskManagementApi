using Microsoft.EntityFrameworkCore;
using taskmanagement.api.ms.Models;

namespace taskmanagement.api.ms.Data
{
    public class TaskManagementDbContext : DbContext
    {
        public DbSet<TaskItem> Tasks => Set<TaskItem>();

        public TaskManagementDbContext(DbContextOptions<TaskManagementDbContext> options)
            : base(options) { }
    }

}
