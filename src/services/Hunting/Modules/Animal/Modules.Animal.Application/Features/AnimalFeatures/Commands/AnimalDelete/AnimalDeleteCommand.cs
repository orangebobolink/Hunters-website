using MediatR;

namespace Modules.Animal.Application.Features.AnimalFeatures.Commands.AnimalDelete
{
    public record AnimalDeleteCommand(
        Guid Id) 
        : IRequest;
}
