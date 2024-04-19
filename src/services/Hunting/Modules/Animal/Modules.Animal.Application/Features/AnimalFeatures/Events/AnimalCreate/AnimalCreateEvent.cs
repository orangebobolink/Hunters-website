using MediatR;
using Modules.Animal.Domain.Entities;

namespace Modules.Animal.Application.Features.AnimalFeatures.Events.AnimalCreate
{
    public record AnimalCreateEvent(
        AnimalInfo Animal) 
        : INotification;
}
