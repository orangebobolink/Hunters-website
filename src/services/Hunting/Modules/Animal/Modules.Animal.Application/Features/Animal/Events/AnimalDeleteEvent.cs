using MediatR;

namespace Modules.Animal.Application.Features.Animal.Events
{
    public record class AnimalDeleteEvent(Guid Id) : INotification;
}
