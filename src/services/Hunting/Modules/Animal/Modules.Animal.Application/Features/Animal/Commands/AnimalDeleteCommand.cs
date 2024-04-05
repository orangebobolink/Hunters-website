using MediatR;

namespace Modules.Animal.Application.Features.Animal.Commands
{
    public record class AnimalDeleteCommand(Guid Id) : IRequest;
}
