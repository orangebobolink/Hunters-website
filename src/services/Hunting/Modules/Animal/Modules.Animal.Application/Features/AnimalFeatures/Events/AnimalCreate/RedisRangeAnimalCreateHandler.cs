using MediatR;
using Modules.Animal.Domain.Entities;
using Modules.Animal.Domain.Helpers;
using Shared.Redis;

namespace Modules.Animal.Application.Features.AnimalFeatures.Events.AnimalCreate
{
    public class RedisRangeAnimalCreateHandler(
        ICacheService cacheService)
        : INotificationHandler<AnimalCreateRangeEvent>
    {
        private readonly ICacheService _cacheService = cacheService;

        public async Task Handle(AnimalCreateRangeEvent notification,
            CancellationToken cancellationToken)
        {
            var cacheKey = CacheHelper.GetCacheKeyForAllAnimals();

            var animals = await _cacheService.GetAsync<List<AnimalInfo>>(
                cacheKey,
                cancellationToken);

            if(animals is null or { Count: 0 })
            {
                await _cacheService.SetDataAsync(cacheKey,
                       notification.Animals,
                       cancellationToken);
            }
        }
    }
}
