using MediatR;
using Modules.Animal.Domain.Entities;

namespace Modules.Animal.Application.Features.Animal.Events.AnimalCreate
{
    public record class AnimalCreateEvent(AnimalInfo Animal) : INotification;
}
