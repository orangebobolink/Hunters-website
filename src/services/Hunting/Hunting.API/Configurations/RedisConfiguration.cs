using Shared.Redis;
using Shared.Redis.Options;

namespace Hunting.API.Configurations
{
    public static class RedisConfiguration
    {
        public static void AddRedisConfiguration(
            this IServiceCollection services, 
            IConfiguration configuration)
        {
            services.AddSharedRedisConfiguration();

            var redisCacheUrl = configuration["Redis:Url"];
            services.AddStackExchangeRedisCache(redisOptions =>
            {
                redisOptions.Configuration = redisCacheUrl;
            });

            var section = configuration.GetSection("redis");
            services.Configure<RedisCacheOptions>(section);
        }
    }
}
