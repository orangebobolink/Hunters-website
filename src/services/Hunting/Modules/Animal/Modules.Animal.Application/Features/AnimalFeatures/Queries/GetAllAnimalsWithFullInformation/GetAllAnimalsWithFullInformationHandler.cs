using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;
using Modules.Animal.Application.Dtos.ResponseDtos;
using Modules.Animal.Application.Features.AnimalFeatures.Queries.GetAllAnimalsWithFullInformation;
using Modules.Animal.Domain.Helpers;
using Modules.Animal.Domain.Interfaces.Repositories;
using Shared.Helpers;

namespace Modules.Animal.Application.Features.Animal.Queries.GetAllAnimalsWithFullInformation
{
    internal class GetAllAnimalsWithFullInformationHandler(
        IAnimalRepository animalRepository, 
        ILogger<GetAllAnimalsWithFullInformationHandler> logger)
        : IRequestHandler<GetAllAnimalsWithFullInformationQuery, List<AnimalInfoResponseDto>>
    {
        private readonly IAnimalRepository _animalRepository = animalRepository;
        private readonly ILogger<GetAllAnimalsWithFullInformationHandler> _logger = logger;

        public async Task<List<AnimalInfoResponseDto>> Handle(GetAllAnimalsWithFullInformationQuery request, 
            CancellationToken cancellationToken)
        {
            var animals = await _animalRepository.GetAllWithFullInformationAsync(cancellationToken);

            if(!animals.Any())
            {
                _logger.LogWarning("No animals found in the database.");
                ThrowHelper.ThrowInvalidOperationException(ErrorMessageHelper.NoAnimalsFound());
            }

            var response = animals.Adapt<List<AnimalInfoResponseDto>>();

            return response;
        }
    }
}
