using TasksManager.Application.BL;
using TasksManager.Domain.Entities;
using TasksManager.Domain.Interfaces.BL;
using TasksManager.Domain.Interfaces.Persistence;
using TasksManager.Domain.Interfaces.Validations;
using TasksManager.Persistence.SQLServer;

namespace TasksManager.API.Startup
{
    internal static class DependencyInjectionConfig
    {
        internal static void AddDependenciesInjectionConfig(this IServiceCollection services)
        {
            services.AddScoped(typeof(IAuthBL), typeof(AuthBL));
            services.AddScoped(typeof(IUserBL), typeof(UserBL)); 

            services.AddScoped(typeof(IUserValidation), typeof(UserValidation));
            services.AddScoped(typeof(IGenericRepository<User>), typeof(UserRepository));

        }
    }
}
