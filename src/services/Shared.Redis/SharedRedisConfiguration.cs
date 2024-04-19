using Microsoft.Extensions.DependencyInjection;

namespace Shared.Redis
{
    public static class SharedRedisConfiguration
    {
        public static void AddSharedRedisConfiguration(
            this IServiceCollection services)
        {
            services.AddSingleton<ICacheService, CacheService>();
        }
    }
}
