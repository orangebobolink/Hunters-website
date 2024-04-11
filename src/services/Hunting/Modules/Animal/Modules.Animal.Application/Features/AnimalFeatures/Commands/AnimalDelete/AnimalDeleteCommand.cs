using MediatR;

namespace Modules.Animal.Application.Features.Animal.Commands.AnimalDelete
{
    public record class AnimalDeleteCommand(Guid Id) : IRequest;
}
