using MediatR;
using Modules.Animal.Application.Dtos.RequestDtos;
using Modules.Animal.Application.Dtos.ResponseDtos;

namespace Modules.Animal.Application.Features.Animal.Commands.AnimalCreate
{
    public record class AnimalCreateCommand(AnimalInfoRequestDto AnimalRequestDto) : IRequest<AnimalInfoResponseDto>;
}
