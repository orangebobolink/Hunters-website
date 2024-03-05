using Identity.Services.Interfaces;
using Identity.Services.Services;
using Identity.Services.Utilities;
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
            services.AddScoped<IRefreshTokenCookie, JwtUtilities>();
            services.AddScoped<IAccessTokenUtilities, JwtUtilities>();
            services.AddScoped<IRefreshTokenUtilities, JwtUtilities>();
            services.AddScoped<ICookieUtilities, CookieUtilities>();
        }
    }
}
