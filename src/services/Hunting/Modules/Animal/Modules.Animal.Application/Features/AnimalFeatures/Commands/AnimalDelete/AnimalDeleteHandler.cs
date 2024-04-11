using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;
using Modules.Animal.Application.Features.Animal.Commands.AnimalCreate;
using Modules.Animal.Application.Features.Animal.Events.AnimalDelete;
using Modules.Animal.Domain.Entities;
using Modules.Animal.Domain.Helpers;
using Modules.Animal.Domain.Interfaces.Repositories;
using Shared.Helpers;

namespace Modules.Animal.Application.Features.Animal.Commands.AnimalDelete
{
    public class AnimalDeleteHandler(IAnimalRepository animalRepository, 
        ILogger<AnimalCreateHandler> logger, 
        IPublisher publisher) 
        : IRequestHandler<AnimalDeleteCommand>
    {
        private readonly IAnimalRepository _animalRepository = animalRepository;
        private readonly ILogger<AnimalCreateHandler> _logger = logger;
        private readonly IPublisher _publisher = publisher;

        public async Task Handle(AnimalDeleteCommand request, CancellationToken cancellationToken)
        {
            Guid id = request.Id;

            var existingAnimal = await _animalRepository.GetByIdAsync(id, cancellationToken);

            if(existingAnimal is null)
            {
                _logger.LogWarning($"Animal with id {id} not found in the database.");
                ThrowHelper.ThrowKeyNotFoundException(ErrorMessageHelper.AnimalNotFound(id));
            }

            var animal = existingAnimal.Adapt<AnimalInfo>();
            _animalRepository.Delete(animal);

            await _animalRepository.SaveChangesAsync(cancellationToken);

            await _publisher.Publish(new AnimalDeleteEvent(id));
        }
    }
}
