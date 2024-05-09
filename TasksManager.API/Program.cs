
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

            builder.Services.AddControllers();
            builder.Services.AddDependenciesInjectionConfig();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbService(config);
            builder.Services.AddAutoMapperConfig();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
