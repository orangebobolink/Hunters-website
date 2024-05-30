using MediatR;
using Modules.Animal.Application.Dtos.ResponseDtos;

namespace Modules.Animal.Application.Features.HuntingSeasonFeatures.Commands.HuntingSeasonDelete
{
    public record HuntingSeasonDeleteCommand(Guid id)
        : IRequest<HuntingSeasonResponseDto>;
}
