using MediatR;
using Modules.Animal.Application.Features.AnimalFeatures.Events.AnimalCreate;
using Modules.Animal.Domain.Entities;
using Modules.Animal.Domain.Helpers;
using Shared.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.Animal.Application.Features.AnimalFeatures.Events.AnimalUpdate
{
    public class RedisRangeAnimalUpdateHandler(
       ICacheService cacheService)
        : INotificationHandler<AnimalUpdateEvent>
    {
        private readonly ICacheService _cacheService = cacheService;

        public async Task Handle(AnimalUpdateEvent notification,
            CancellationToken cancellationToken)
        {
            var cacheKey = CacheHelper.GetCacheKeyForAllAnimals();
            var cacheFullKey = CacheHelper.GetCacheKeyForAllAnimalsWithFullInformation();

            await _cacheService.RemoveDataAsync(cacheKey, cancellationToken);
            await _cacheService.RemoveDataAsync(cacheFullKey, cancellationToken);
        }
    }
}
