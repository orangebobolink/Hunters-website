using MediatR;
using Modules.Animal.Application.Dtos.RequestDtos;
using Modules.Animal.Application.Dtos.ResponseDtos;

namespace Modules.Animal.Application.Features.AnimalFeatures.Commands.AnimalUpdate
{
    public record AnimalUpdateCommand(Guid Id, AnimalInfoRequestDto AnimalRequestDto) : IRequest<AnimalInfoResponseDto>;
}
