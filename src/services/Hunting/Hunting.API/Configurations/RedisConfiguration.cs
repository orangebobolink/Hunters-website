using Microsoft.Extensions.Caching.Redis;

namespace Hunting.API.Configurations
{
    public static class RedisConfiguration
    {
        public static void AddRedisConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var redisCacheUrl = configuration["Redis:Url"];

            services.AddStackExchangeRedisCache(redisOptions =>
            {
                redisOptions.Configuration = redisCacheUrl;
            });

            services.Configure<RedisCacheOptions>(
                configuration.GetSection("redis")
                );
        }
    }
}
