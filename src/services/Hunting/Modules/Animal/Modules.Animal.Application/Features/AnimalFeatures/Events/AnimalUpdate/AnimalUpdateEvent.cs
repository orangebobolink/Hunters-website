using MediatR;
using Modules.Animal.Domain.Entities;

namespace Modules.Animal.Application.Features.AnimalFeatures.Events.AnimalUpdate
{
    public record AnimalUpdateEvent(
        AnimalInfo Animal) 
        : INotification;
}
