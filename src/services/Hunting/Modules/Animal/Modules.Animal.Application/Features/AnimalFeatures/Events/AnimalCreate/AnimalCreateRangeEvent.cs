using MediatR;
using Modules.Animal.Domain.Entities;

namespace Modules.Animal.Application.Features.AnimalFeatures.Events.AnimalCreate
{
    public record AnimalCreateRangeEvent(
        List<AnimalInfo> Animals) 
        : INotification;
}
