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
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IAuthorizationService, AuthorizationService>();
            services.AddScoped<IRefreshTokenCookie, RefreshTokenUtility>();
            services.AddScoped<IRefreshTokenUtilities, RefreshTokenUtility>();
            services.AddScoped<IAccessTokenUtilities, AccessTokenUtility>();
            services.AddScoped<ICookieUtilities, CookieUtility>();
        }
    }
}
