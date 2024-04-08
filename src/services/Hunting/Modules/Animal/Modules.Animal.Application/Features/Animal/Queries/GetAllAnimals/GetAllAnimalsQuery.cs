using MediatR;
using Modules.Animal.Application.Dtos.ResponseDtos;

namespace Modules.Animal.Application.Features.Animal.Queries.GetAllAnimals
{
    public record class GetAllAnimalsQuery() : IRequest<List<AnimalInfoResponseDto>>;
}
