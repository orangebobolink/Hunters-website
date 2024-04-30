using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;
using Modules.Animal.Application.Dtos.ResponseDtos;
using Modules.Animal.Domain.Entities;
using Modules.Animal.Domain.Helpers;
using Modules.Animal.Domain.Interfaces.Repositories;
using Shared.Helpers;
using Shared.Redis;

namespace Modules.Animal.Application.Features.HuntingSeasonFeatures.Commands.HuntingSeasonDelete
{
    public class HuntingSeasonDeleteHandler(
         IHuntingSeasonRepository huntingSeasonRepository,
         ILogger<HuntingSeasonDeleteHandler> logger,
         IPublisher publisher,
         ICacheService cacheService)
         : IRequestHandler<HuntingSeasonDeleteCommand, HuntingSeasonResponseDto>
    {
        private readonly IHuntingSeasonRepository _huntingSeasonRepository = huntingSeasonRepository;
        private readonly ILogger<HuntingSeasonDeleteHandler> _logger = logger;
        private readonly IPublisher _publisher = publisher;
        private readonly ICacheService _cacheService = cacheService;

        public async Task<HuntingSeasonResponseDto> Handle(HuntingSeasonDeleteCommand request, CancellationToken cancellationToken)
        {
            var id = request.id;

            var existingHuntingSeason = await _huntingSeasonRepository.GetByIdAsync(
                id,
                cancellationToken);

            if (existingHuntingSeason is null)
            {
                ThrowHelper.ThrowInvalidOperationException(" ");
            }

            _huntingSeasonRepository.Delete(existingHuntingSeason!);

            await _huntingSeasonRepository.SaveChangesAsync(cancellationToken);

            var cacheKey = CacheHelper.GetCacheKeyForAllAnimals();
            var cacheFullKey = CacheHelper.GetCacheKeyForAllAnimalsWithFullInformation();

            await _cacheService.RemoveDataAsync(cacheKey, cancellationToken);
            await _cacheService.RemoveDataAsync(cacheFullKey, cancellationToken);

            var response = existingHuntingSeason!.Adapt<HuntingSeasonResponseDto>();

            return response;
        }
    }
}
