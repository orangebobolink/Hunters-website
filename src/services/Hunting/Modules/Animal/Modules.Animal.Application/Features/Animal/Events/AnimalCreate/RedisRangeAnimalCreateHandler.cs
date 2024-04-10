using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Redis;
using Microsoft.Extensions.Options;

namespace Modules.Animal.Application.Features.Animal.Events.AnimalCreate
{
    internal class RedisRangeAnimalCreateHandler 
        : INotificationHandler<AnimalCreateRangeEvent>
    {
        private readonly IDistributedCache _cache;
        private readonly IOptions<RedisCacheOptions> _cacheOptions;

        public Task Handle(AnimalCreateRangeEvent notification, 
            CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
