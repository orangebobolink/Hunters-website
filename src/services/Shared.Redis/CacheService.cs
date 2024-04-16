using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Shared.Redis
{
    internal class CacheService(
        IDistributedCache cache) 
        : ICacheService
    {
        private readonly IDistributedCache _cache = cache;

        public async Task<T?> GetAsync<T>(string key, 
            CancellationToken cancellationToken) 
            where T : class
        {
            string? cachedValue = await _cache.GetStringAsync(
                key, 
                cancellationToken);

            if(cachedValue is null)
                return null;

            T? value = JsonConvert.DeserializeObject<T>(cachedValue);

            return value;
        }

        public async Task SetData<T>(string key, 
            T value, 
            CancellationToken cancellationToken) 
            where T : class
        {
            string cachedValue = JsonConvert.SerializeObject(value);

            await _cache.SetStringAsync(key, cachedValue, cancellationToken);
        }

        public async Task RemoveData(string key, 
            CancellationToken cancellationToken)
        {
            await _cache.RemoveAsync(key, cancellationToken);
        }

        public async Task RefreshData<T>(string key, 
            T value, 
            CancellationToken cancellationToken)
            where T : class
        {
            T? existingData = await GetAsync<T>(key, cancellationToken);

            if (existingData is not null)
            {
                await RemoveData(key, cancellationToken);
            }

            await SetData(key, value, cancellationToken);
        }
    }
}
