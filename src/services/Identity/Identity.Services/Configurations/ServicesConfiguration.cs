using Identity.Services.Interfaces;
using Identity.Services.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Services.Configurations
{
    public static class ServicesConfiguration
    {
        public static void AddServicesConfiguration(this IServiceCollection services)
        {
            services.AddScoped<IAuthorizationService, AuthorizationService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITokenService, TokenService>();
        }
    }
}
