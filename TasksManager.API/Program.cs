
using TasksManager.API.Startup;

namespace TasksManager.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            
            var builder = WebApplication.CreateBuilder(args);
            IConfiguration config = builder.Configuration.AddJsonFile("appsettings.json").Build();
            // Add services to the container.

            builder.Services.AddCorsDocumentation();
            builder.Services.AddControllers();
            builder.Services.AddDependenciesInjectionConfig();
            builder.Services.AddJwtConfig(config);
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerConfig();
            builder.Services.AddDbService(config);
            builder.Services.AddAutoMapperConfig();
            var app = builder.Build();

            app.UseCorsDocumentation();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            //Middlewares
            app.UseExceptionMiddleware();
            app.UseJwtConfig();
            app.UseAuthorization();
            
            app.MapControllers();
            app.Run();
        }
    }
}
