
using TasksManager.Domain.Variables;

namespace TasksManager.API.Startup
{
    internal static class CorsConfig
    {
        internal static IServiceCollection AddCorsDocumentation(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(AppSettingsKeys.CORS_NAME, builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();

                });
            });
            return services;
        }

        internal static IApplicationBuilder UseCorsDocumentation(this IApplicationBuilder app)
        {
            app.UseCors(AppSettingsKeys.CORS_NAME);
            return app;
        }
    }
}