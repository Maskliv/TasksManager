

using AutoMapper;
using Microsoft.Extensions.Configuration;
using TasksManager.API.Startup;

namespace TasksManager.Tests
{
    
    public class TestStartup
    {
        public IConfiguration Config { get;}
        public IMapper Mapper { get; }

        public TestStartup()
        {
            Config = new ConfigurationBuilder().SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../../../../TasksManager.API"))
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();
            
            var mapperConfig = new MapperConfiguration(mc => mc.AddProfile(new MappingProfile()));
            Mapper = mapperConfig.CreateMapper();

        }
        
    }
}