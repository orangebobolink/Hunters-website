using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;
using Modules.Animal.Application.Dtos.ResponseDtos;
using Modules.Animal.Application.Features.Animal.Events.AnimalCreate;
using Modules.Animal.Domain.Entities;
using Modules.Animal.Domain.Helpers;
using Modules.Animal.Domain.Interfaces.Repositories;
using Shared.Helpers;

namespace Modules.Animal.Application.Features.Animal.Commands.AnimalCreate
{
    public class AnimalCreateHandler(IAnimalRepository animalRepository, 
        ILogger<AnimalCreateHandler> logger, 
        IPublisher publishe) 
        : IRequestHandler<AnimalCreateCommand, AnimalInfoResponseDto>
    {
        private readonly IAnimalRepository _animalRepository = animalRepository;
        private readonly ILogger<AnimalCreateHandler> _logger = logger;
        private readonly IPublisher _publisher = publishe;

        public async Task<AnimalInfoResponseDto> Handle(AnimalCreateCommand request, CancellationToken cancellationToken)
        {
            var animalRequestDto = request.AnimalRequestDto;

            var existingAnimal = await _animalRepository.GetByNameAsync(animalRequestDto.Name, cancellationToken);

            if(existingAnimal is not null)
            {
                _logger.LogInformation($"Animal with name {animalRequestDto.Name} already exists.");
                ThrowHelper.ThrowInvalidOperationException(
                    ErrorMessageHelper.AnimalAlreadyExists(animalRequestDto.Name));
            }

            var animal = animalRequestDto.Adapt<AnimalInfo>();
            animal.Id = Guid.NewGuid();
            _animalRepository.Create(animal);

            await _animalRepository.SaveChangesAsync(cancellationToken);

            await _publisher.Publish(new AnimalCreateEvent(animal), cancellationToken);

            var response = animal.Adapt<AnimalInfoResponseDto>();
             
            return response;
        }
    }
}
