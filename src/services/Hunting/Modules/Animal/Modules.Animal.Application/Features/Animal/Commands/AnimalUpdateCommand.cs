using MediatR;
using Modules.Animal.Application.Dtos.RequestDtos;
using Modules.Animal.Application.Dtos.ResponseDtos;

namespace Modules.Animal.Application.Features.Animal.Commands
{
    public record class AnimalUpdateCommand(Guid Id, AnimalInfoRequestDto AnimalRequestDto) : IRequest<AnimalInfoResponseDto>;
}
