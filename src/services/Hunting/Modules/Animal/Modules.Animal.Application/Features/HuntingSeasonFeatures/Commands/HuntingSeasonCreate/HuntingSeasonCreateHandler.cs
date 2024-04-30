using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;
using Modules.Animal.Application.Dtos.ResponseDtos;
using Modules.Animal.Application.Features.AnimalFeatures.Events.AnimalUpdate;
using Modules.Animal.Domain.Entities;
using Modules.Animal.Domain.Helpers;
using Modules.Animal.Domain.Interfaces.Repositories;
using Shared.Helpers;
using Shared.Redis;

namespace Modules.Animal.Application.Features.HuntingSeasonFeatures.Commands.HuntingSeasonCreate
{
    public class HuntingSeasonCreateHandler(
        IHuntingSeasonRepository huntingSeasonRepository,
        ILogger<HuntingSeasonCreateHandler> logger,
        IPublisher publisher,
        ICacheService cacheService)
        : IRequestHandler<HuntingSeasonCreateCommand, HuntingSeasonResponseDto>
    {
        private readonly IHuntingSeasonRepository _huntingSeasonRepository = huntingSeasonRepository;
        private readonly ILogger<HuntingSeasonCreateHandler> _logger = logger;
        private readonly IPublisher _publisher = publisher;
        private readonly ICacheService _cacheService = cacheService;

        public async Task<HuntingSeasonResponseDto> Handle(HuntingSeasonCreateCommand request, CancellationToken cancellationToken)
        {
            var huntingSeasonRequest = request.HuntingSeasonRequest;

            var existingHuntingSeason = await _huntingSeasonRepository.GetHuntingSeasonByTimePeriodForAnimal(
                huntingSeasonRequest.AnimalId,
                huntingSeasonRequest.StartDate,
                huntingSeasonRequest.EndDate,
                cancellationToken);

            if (existingHuntingSeason is not null)
            {
                _logger.LogInformation($"Hunting season conflict: Animal with ID {huntingSeasonRequest.AnimalId}, " +
                                       $"StartDate '{huntingSeasonRequest.StartDate}', " +
                                       $"EndDate '{huntingSeasonRequest.EndDate}' already has an existing hunting season.");
                ThrowHelper.ThrowInvalidOperationException(" ");
            }

            var huntingSeason = huntingSeasonRequest.Adapt<HuntingSeason>();
            huntingSeason.Id = Guid.NewGuid();
            _huntingSeasonRepository.Create(huntingSeason);

            await _huntingSeasonRepository.SaveChangesAsync(cancellationToken);

            var cacheKey = CacheHelper.GetCacheKeyForAllAnimals();
            var cacheFullKey = CacheHelper.GetCacheKeyForAllAnimalsWithFullInformation();

            await _cacheService.RemoveDataAsync(cacheKey, cancellationToken);
            await _cacheService.RemoveDataAsync(cacheFullKey, cancellationToken);

            var response = huntingSeason.Adapt<HuntingSeasonResponseDto>();

            return response;
        }
    }
}
