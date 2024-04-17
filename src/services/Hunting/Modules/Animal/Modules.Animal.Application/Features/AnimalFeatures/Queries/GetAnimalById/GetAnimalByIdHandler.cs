using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;
using Modules.Animal.Application.Dtos.ResponseDtos;
using Modules.Animal.Domain.Entities;
using Modules.Animal.Domain.Helpers;
using Modules.Animal.Domain.Interfaces.Repositories;
using Shared.Helpers;
using Shared.Redis;

namespace Modules.Animal.Application.Features.AnimalFeatures.Queries.GetAnimalById
{
    internal class GetAnimalByIdHandler(IAnimalRepository animalRepository, ILogger<GetAnimalByIdHandler> logger, ICacheService cacheService)
                : IRequestHandler<GetAnimalByIdQuery, AnimalInfoResponseDto>
    {
        private readonly IAnimalRepository _animalRepository = animalRepository;
        private readonly ILogger<GetAnimalByIdHandler> _logger = logger;
        private readonly ICacheService _cacheService = cacheService;

        public async Task<AnimalInfoResponseDto> Handle(GetAnimalByIdQuery request, CancellationToken cancellationToken)
        {
            var id = request.Id;

            var animal = await _cacheService.GetAsync(CacheHelper.GetCacheKeyForAllAnimals(),
                async () => await GetAnimalThroughRepository(id, cancellationToken),
                cancellationToken);

            var response = animal.Adapt<AnimalInfoResponseDto>();

            return response;
        }

        private async Task<AnimalInfo> GetAnimalThroughRepository(
            Guid id,
            CancellationToken cancellationToken)
        {
            var animal = await _animalRepository.GetByIdAsync(id, cancellationToken);

            if(animal is null)
            {
                _logger.LogWarning($"An animal with id {id} not found in the database.");
                ThrowHelper.ThrowKeyNotFoundException(ErrorMessageHelper.AnimalNotFound(id));
            }

            return animal!;
        }
    }
}
