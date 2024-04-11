using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;
using Modules.Animal.Application.Dtos.ResponseDtos;
using Modules.Animal.Application.Features.Animal.Commands.AnimalCreate;
using Modules.Animal.Domain.Helpers;
using Modules.Animal.Domain.Interfaces.Repositories;
using Shared.Helpers;

namespace Modules.Animal.Application.Features.Animal.Queries.GetAllAnimals
{
    internal class GetAllAnimalsHandler(IAnimalRepository animalRepository, 
        ILogger<GetAllAnimalsHandler> logger) 
        : IRequestHandler<GetAllAnimalsQuery, List<AnimalInfoResponseDto>>
    {
        private readonly IAnimalRepository _animalRepository = animalRepository;
        private readonly ILogger<GetAllAnimalsHandler> _logger = logger;

        public async Task<List<AnimalInfoResponseDto>> Handle(GetAllAnimalsQuery request, CancellationToken cancellationToken)
        {
            var animals = await _animalRepository.GetAllAsync(cancellationToken);

            if(animals is null)
            {
                _logger.LogWarning("No animals found in the database.");
                ThrowHelper.ThrowInvalidOperationException(ErrorMessageHelper.NoAnimalsFound());
            }

            var response = animals.Adapt<List<AnimalInfoResponseDto>>();

            return response;
        }
    }
}
