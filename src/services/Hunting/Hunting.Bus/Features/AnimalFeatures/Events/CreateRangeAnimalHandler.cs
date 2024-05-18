using Mapster;
using MediatR;
using Modules.Animal.Application.Features.AnimalFeatures.Events.AnimalCreate;
using Modules.Document.Domain.Entities;
using Modules.Document.Domain.Interfaces;

namespace Hunting.Bus.Features.AnimalFeatures.Events
{
    public class CreateRangeAnimalHandler(
        IAnimalRepository animalRepository)
        : INotificationHandler<AnimalCreateRangeEvent>
    {
        private readonly IAnimalRepository _animalRepository = animalRepository;

        public async Task Handle(AnimalCreateRangeEvent notification,
            CancellationToken cancellationToken)
        {
            if (!(await _animalRepository.GetAllAsync(cancellationToken)).Any())
            {
                var animals = notification.Animals.Adapt<List<Animal>>();
                animals.ForEach(_animalRepository.Create);

                await _animalRepository.SaveChangesAsync(CancellationToken.None);
            }
        }
    }
}
