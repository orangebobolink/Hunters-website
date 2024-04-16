using MediatR;
using Microsoft.Extensions.Caching.Distributed;

using Modules.Animal.Domain.Helpers;
using Shared.Redis;

namespace Modules.Animal.Application.Features.AnimalFeatures.Events.AnimalCreate
{
    public class RedisRangeAnimalCreateHandler(
        ICacheService cacheService)
        : INotificationHandler<AnimalCreateRangeEvent>
    {
        private readonly ICacheService _cacheService = cacheService;

        public Task Handle(AnimalCreateRangeEvent notification, 
            CancellationToken cancellationToken)
        {
            var cacheKey = CacheHelper.GetCacheKeyForAllAnimals();
            _cacheService.SetData(cacheKey, notification.Animals, cancellationToken);

            return Task.CompletedTask;
        }
    }
}
