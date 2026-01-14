using Microsoft.EntityFrameworkCore;
using taskmanagement.api.ms.Models;

namespace taskmanagement.api.ms.domain.Interface
{
    public interface ITaskDbContext
    {
        DbSet<TaskItem> Tasks { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
