using MediatR;
using Modules.Animal.Application.Dtos.RequestDtos;
using Modules.Animal.Application.Dtos.ResponseDtos;

namespace Modules.Animal.Application.Features.HuntingSeason.Commands.HuntingSeasonCreate
{
    public record class HuntingSeasonCreateCommand(HuntingSeasonRequestDto HuntingSeasonRequest) : IRequest<HuntingSeasonResponseDto>;
}
