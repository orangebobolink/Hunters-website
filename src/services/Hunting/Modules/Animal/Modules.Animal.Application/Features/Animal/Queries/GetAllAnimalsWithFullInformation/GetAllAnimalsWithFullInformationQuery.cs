using MediatR;
using Modules.Animal.Application.Dtos.ResponseDtos;

namespace Modules.Animal.Application.Features.Animal.Queries.GetAllAnimalsWithFullInformation
{
    public record class GetAllAnimalsWithFullInformationQuery() : IRequest<List<AnimalInfoResponseDto>>;
}
