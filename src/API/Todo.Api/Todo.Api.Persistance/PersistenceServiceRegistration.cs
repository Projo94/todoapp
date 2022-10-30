using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Todo.Api.Application.Contracts.Persistence;
using Todo.Api.Persistance.Repositories;

namespace Todo.Api.Persistance
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<TodoApiDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("TodoApiConnectionString")));

            services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<>));
            services.AddScoped<ITaskListRepository, TaskListRepository>();
            services.AddScoped<ITaskRepository, TaskRepository>();

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}