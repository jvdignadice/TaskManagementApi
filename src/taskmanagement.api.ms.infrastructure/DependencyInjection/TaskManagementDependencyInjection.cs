using Microsoft.Extensions.DependencyInjection;
using taskmanagement.api.ms.domain.Interface;
using taskmanagement.api.ms.domain.Service;

namespace taskmanagement.api.ms.infrastructure.DependencyInjection
{
    public static class TaskManagementDependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<ITaskService, TaskService>();
            return services;
        }
    }
}
