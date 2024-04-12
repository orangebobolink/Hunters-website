using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Redis;
using Microsoft.Extensions.Options;
using Modules.Animal.Domain.Helpers;

namespace Modules.Animal.Application.Features.AnimalFeatures.Events.AnimalCreate
{
    internal class RedisRangeAnimalCreateHandler(IDistributedCache cache, IOptions<RedisCacheOptions> cacheOptions)
                : INotificationHandler<AnimalCreateRangeEvent>
    {
        private readonly IDistributedCache _cache = cache;
        private readonly IOptions<RedisCacheOptions> _cacheOptions = cacheOptions;

        public Task Handle(AnimalCreateRangeEvent notification, 
            CancellationToken cancellationToken)
        {
            var cacheKey = CacheHelper.GetCacheKeyForAllAnimals();
            //var isExerciseInCache = _cache.TryGetValue(cacheKey, out foundExercise);
            return Task.CompletedTask;
        }
    }
}
