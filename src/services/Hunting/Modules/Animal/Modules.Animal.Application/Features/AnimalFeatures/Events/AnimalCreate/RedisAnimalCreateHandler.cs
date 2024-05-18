using MediatR;
using Modules.Animal.Domain.Helpers;
using Shared.Redis;

namespace Modules.Animal.Application.Features.AnimalFeatures.Events.AnimalCreate
{
    public class RedisAnimalCreateHandler(
        ICacheService cacheService)
        : INotificationHandler<AnimalCreateEvent>
    {
        private readonly ICacheService _cacheService = cacheService;

        public async Task Handle(AnimalCreateEvent notification,
            CancellationToken cancellationToken)
        {
            var cacheKey = CacheHelper.GetCacheKeyForAllAnimals();
            var cacheFullKey = CacheHelper.GetCacheKeyForAllAnimalsWithFullInformation();

            await _cacheService.RemoveDataAsync(cacheKey, cancellationToken);
            await _cacheService.RemoveDataAsync(cacheFullKey, cancellationToken);
        }
    }
}
