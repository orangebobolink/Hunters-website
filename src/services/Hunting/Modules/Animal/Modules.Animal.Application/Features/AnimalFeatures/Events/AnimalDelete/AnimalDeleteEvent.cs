using MediatR;

namespace Modules.Animal.Application.Features.Animal.Events.AnimalDelete
{
    public record class AnimalDeleteEvent(Guid Id) : INotification;
}
