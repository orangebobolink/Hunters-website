using Mapster;
using MediatR;
using Modules.Animal.Application.Features.AnimalFeatures.Events.AnimalCreate;
using Modules.Document.Domain.Entities;
using Modules.Document.Domain.Interfaces;

namespace Hunting.Bus.Features.AnimalFeatures.Events
{
    internal class CreateAnimalHandler(
        IAnimalRepository animalRepository)
        : INotificationHandler<AnimalCreateEvent>
    {
        private readonly IAnimalRepository _animalRepository = animalRepository;

        public async Task Handle(
            AnimalCreateEvent notification,
            CancellationToken cancellationToken)
        {
            var animal = notification.Animal
                                    .Adapt<Animal>();
            _animalRepository.Create(animal);

            await _animalRepository.SaveChangesAsync(CancellationToken.None);
        }
    }
}
