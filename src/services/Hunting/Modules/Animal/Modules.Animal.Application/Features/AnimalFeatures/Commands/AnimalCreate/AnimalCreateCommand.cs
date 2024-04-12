using MediatR;
using Modules.Animal.Application.Dtos.RequestDtos;
using Modules.Animal.Application.Dtos.ResponseDtos;

namespace Modules.Animal.Application.Features.AnimalFeatures.Commands.AnimalCreate
{
    public record AnimalCreateCommand(
        AnimalInfoRequestDto AnimalRequestDto) 
        : IRequest<AnimalInfoResponseDto>;
}
