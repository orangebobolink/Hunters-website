using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;
using Modules.Animal.Application.Dtos.ResponseDtos;
using Modules.Animal.Application.Features.AnimalFeatures.Commands.AnimalCreate;
using Modules.Animal.Application.Features.AnimalFeatures.Events.AnimalUpdate;
using Modules.Animal.Domain.Helpers;
using Modules.Animal.Domain.Interfaces.Repositories;
using Shared.Helpers;

namespace Modules.Animal.Application.Features.AnimalFeatures.Commands.AnimalUpdate
{
    internal class AnimalUpdateHandler(
        IAnimalRepository animalRepository,
        ILogger<AnimalCreateHandler> logger,
        IPublisher publisher)
        : IRequestHandler<AnimalUpdateCommand, AnimalInfoResponseDto>
    {
        private readonly IAnimalRepository _animalRepository = animalRepository;
        private readonly ILogger<AnimalCreateHandler> _logger = logger;
        private readonly IPublisher _publisher = publisher;

        public async Task<AnimalInfoResponseDto> Handle(AnimalUpdateCommand request, CancellationToken cancellationToken)
        {
            var id = request.Id;
            var animalRequestDto = request.AnimalRequestDto;

            var existingAnimal = await _animalRepository.GetByIdAsync(id, cancellationToken);

            if (existingAnimal is null)
            {
                _logger.LogWarning($"An animal with id {id} not found in the database.");
                ThrowHelper.ThrowKeyNotFoundException(ErrorMessageHelper.AnimalNotFound(id));
            }

            var animal = animalRequestDto.Adapt(existingAnimal);
            _animalRepository.Update(animal!);

            await _animalRepository.SaveChangesAsync(cancellationToken);

            var updatedAnimal = await _animalRepository.GetByNameAsync(animalRequestDto.Name, cancellationToken);

            await _publisher.Publish(new AnimalUpdateEvent(updatedAnimal!), cancellationToken);

            var response = updatedAnimal.Adapt<AnimalInfoResponseDto>();

            return response;
        }
    }
}
