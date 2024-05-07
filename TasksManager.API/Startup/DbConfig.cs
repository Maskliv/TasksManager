using Microsoft.EntityFrameworkCore;
using TasksManager.Domain.Variables;
using TasksManager.Persistence;

namespace TasksManager.API.Startup
{
    internal static class DbConfig
    {
        internal static IServiceCollection AddDbService(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(config.GetConnectionString(AppSettingsKeys.DB_CONNECTION) ?? 
                throw new Exception("ConnectionString in appsettings.json has not been setted yet") ));

            return services;
        }

    }
}
