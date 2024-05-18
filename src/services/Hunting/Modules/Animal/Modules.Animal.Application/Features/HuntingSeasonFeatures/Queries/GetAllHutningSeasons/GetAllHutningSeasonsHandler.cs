using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;
using Modules.Animal.Application.Dtos.ResponseDtos;
using Modules.Animal.Domain.Entities;
using Modules.Animal.Domain.Helpers;
using Modules.Animal.Domain.Interfaces.Repositories;
using Shared.Helpers;
using Shared.Redis;

namespace Modules.Animal.Application.Features.HuntingSeasonFeatures.Queries.GetAllHutningSeasons
{
    internal class GetAllHutningSeasonsHandler(IHuntingSeasonRepository huntingSeasonRepository, ILogger<GetAllHutningSeasonsHandler> logger, ICacheService cacheService)
                : IRequestHandler<GetAllHutningSeasonsQuery, List<HuntingSeasonResponseDto>>
    {
        private readonly IHuntingSeasonRepository _huntingSeasonRepository = huntingSeasonRepository;
        private readonly ILogger<GetAllHutningSeasonsHandler> _logger = logger;
        private readonly ICacheService _cacheService = cacheService;

        public async Task<List<HuntingSeasonResponseDto>> Handle(
            GetAllHutningSeasonsQuery request, 
            CancellationToken cancellationToken)
        {
            var animals = await _cacheService.GetAsync(CacheHelper.GetCacheKeyForAllAnimals(),
                async () => await GetAnimalsThroughRepository(cancellationToken), 
                cancellationToken);

            var response = animals.Adapt<List<HuntingSeasonResponseDto>>();

            return response;
        }

        private async Task<List<HuntingSeason>> GetAnimalsThroughRepository(CancellationToken cancellationToken)
        {
            var huntingSeasons = await _huntingSeasonRepository.GetAllAsync(cancellationToken);

            if(!huntingSeasons.Any())
            {
                _logger.LogWarning("No hunting seasons found in the database.");
                ThrowHelper.ThrowInvalidOperationException(ErrorMessageHelper.NoAnimalsFound());
            }

            return huntingSeasons;
        }
    }
}