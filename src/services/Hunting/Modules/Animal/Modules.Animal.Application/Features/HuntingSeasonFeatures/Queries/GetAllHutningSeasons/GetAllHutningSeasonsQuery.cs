using MediatR;
using Modules.Animal.Application.Dtos.ResponseDtos;

namespace Modules.Animal.Application.Features.HuntingSeasonFeatures.Queries.GetAllHutningSeasons
{
    public record GetAllHutningSeasonsQuery : IRequest<List<HuntingSeasonResponseDto>>;
}
