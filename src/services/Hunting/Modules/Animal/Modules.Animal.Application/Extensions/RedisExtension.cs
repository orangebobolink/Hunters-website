using Microsoft.Extensions.Caching.Distributed;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace Modules.Animal.Application.Extensions
{
    public static class RedisExtension
    {
        public static Task SetAsync<T>(this IDistributedCache cache, string key, T value, DistributedCacheEntryOptions options)
        {
            var bytes = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(value, GetJsonSerializerOptions()));
            
            return cache.SetAsync(key, bytes, options);
        }

        public static bool TryGetValue<T>(this IDistributedCache cache, string key, out T? value)
        {
            var result = cache.Get(key);
            value = default;

            if(result is null)
            {
                return false;
            }

            value = JsonSerializer.Deserialize<T>(result, GetJsonSerializerOptions());

            return true;
        }

        private static JsonSerializerOptions GetJsonSerializerOptions()
        {
            return new JsonSerializerOptions()
            {
                PropertyNamingPolicy = null,
                WriteIndented = true,
                AllowTrailingCommas = true,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                ReferenceHandler = ReferenceHandler.Preserve
            };
        }
    }
}
