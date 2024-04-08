using MediatR;
using Modules.Animal.Domain.Entities;

namespace Modules.Animal.Application.Features.Animal.Events.AnimalUpdate
{
    public record class AnimalUpdateEvent(AnimalInfo Animal) : INotification;
}
