using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using TasksManager.Domain.Variables;

namespace TasksManager.API.Startup
{
    internal static class JwtConfig
    {
        internal static IServiceCollection AddJwtConfig(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.SaveToken = true;
                x.RequireHttpsMetadata = false;
                x.TokenValidationParameters = GetTokenValidationParameters(configuration);
            });
            return services;
        }

        internal static TokenValidationParameters GetTokenValidationParameters(IConfiguration configuration)
        {
            return new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration[AppSettingsKeys.JWT_SECRET]?? throw new Exception("JWT Secret not setted yet."))),
                ValidateLifetime = true,
            };
        }

        internal static IApplicationBuilder UseJwtConfig(this IApplicationBuilder app)
        {
            return app.UseAuthentication();
        }
    }
}