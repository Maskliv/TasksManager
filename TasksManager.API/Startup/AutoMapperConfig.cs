using AutoMapper;
using TasksManager.Domain.DTO;
using TasksManager.Domain.Entities;

namespace TasksManager.API.Startup
{
    internal static class AutoMapperConfig
    {
        internal static IServiceCollection AddAutoMapperConfig(this IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(mc => mc.AddProfile(new MappingProfile()));
            IMapper mapper = mapperConfig.CreateMapper();
            return services.AddSingleton(mapper);
        }
        
    }

    internal class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
            CreateMap<AppTask, AppTaskDto>();
            CreateMap<AppTaskDto, AppTask>();
        }
    }
}