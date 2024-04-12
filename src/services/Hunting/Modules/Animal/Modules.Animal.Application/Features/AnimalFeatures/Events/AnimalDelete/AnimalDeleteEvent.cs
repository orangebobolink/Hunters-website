using MediatR;

namespace Modules.Animal.Application.Features.AnimalFeatures.Events.AnimalDelete
{
    public record AnimalDeleteEvent(Guid Id) : INotification;
}
