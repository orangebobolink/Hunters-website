using MediatR;
using Modules.Animal.Application.Dtos.ResponseDtos;

namespace Modules.Animal.Application.Features.AnimalFeatures.Queries.GetAllAnimalsWithFullInformation
{
    public record GetAllAnimalsWithFullInformationQuery() 
        : IRequest<List<AnimalInfoResponseDto>>;
}
