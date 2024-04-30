using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;
using Modules.Animal.Application.Dtos.ResponseDtos;
using Modules.Animal.Domain.Entities;
using Modules.Animal.Domain.Helpers;
using Modules.Animal.Domain.Interfaces.Repositories;
using Shared.Helpers;
using Shared.Redis;

namespace Modules.Animal.Application.Features.AnimalFeatures.Queries.GetAllAnimalsWithFullInformation
{
    internal class GetAllAnimalsWithFullInformationHandler(
        IAnimalRepository animalRepository,
        ILogger<GetAllAnimalsWithFullInformationHandler> logger,
        ICacheService cacheService)
        : IRequestHandler<GetAllAnimalsWithFullInformationQuery, List<AnimalInfoResponseDto>>
    {
        private readonly IAnimalRepository _animalRepository = animalRepository;
        private readonly ILogger<GetAllAnimalsWithFullInformationHandler> _logger = logger;
        private readonly ICacheService _cacheService = cacheService;

        public async Task<List<AnimalInfoResponseDto>> Handle(
            GetAllAnimalsWithFullInformationQuery request,
            CancellationToken cancellationToken)
        {
            var animals = await _cacheService.GetAsync(CacheHelper.GetCacheKeyForAllAnimalsWithFullInformation(),
                async () => await GetAnimalsThroughRepository(cancellationToken),
                cancellationToken);

            var response = animals.Adapt<List<AnimalInfoResponseDto>>();

            return response;
        }

        private async Task<List<AnimalInfo>> GetAnimalsThroughRepository(
            CancellationToken cancellationToken)
        {
            var animals = await _animalRepository.GetAllWithFullInformationAsync(cancellationToken);

            if (!animals.Any())
            {
                _logger.LogWarning("No animals found in the database.");
                ThrowHelper.ThrowInvalidOperationException(ErrorMessageHelper.NoAnimalsFound());
            }

            return animals;
        }
    }
}
