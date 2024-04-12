using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;
using Modules.Animal.Application.Dtos.ResponseDtos;
using Modules.Animal.Domain.Helpers;
using Modules.Animal.Domain.Interfaces.Repositories;
using Shared.Helpers;

namespace Modules.Animal.Application.Features.AnimalFeatures.Queries.GetAnimalById
{
    internal class GetAnimalByIdHandler(IAnimalRepository animalRepository, 
        ILogger<GetAnimalByIdHandler> logger) 
        : IRequestHandler<GetAnimalByIdQuery, AnimalInfoResponseDto>
    {
        private readonly IAnimalRepository _animalRepository = animalRepository;
        private readonly ILogger<GetAnimalByIdHandler> _logger = logger;

        public async Task<AnimalInfoResponseDto> Handle(GetAnimalByIdQuery request, CancellationToken cancellationToken)
        {
            var id = request.Id;

            var animal = await _animalRepository.GetByIdAsync(id, cancellationToken);

            if(animal is null)
            {
                _logger.LogWarning($"An animal with id {id} not found in the database.");
                ThrowHelper.ThrowKeyNotFoundException(ErrorMessageHelper.AnimalNotFound(id));
            }

            var response = animal.Adapt<AnimalInfoResponseDto>();

            return response;
        }
    }
}
