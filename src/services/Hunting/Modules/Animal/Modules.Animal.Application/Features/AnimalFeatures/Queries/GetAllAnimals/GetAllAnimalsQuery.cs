using MediatR;
using Modules.Animal.Application.Dtos.ResponseDtos;

namespace Modules.Animal.Application.Features.AnimalFeatures.Queries.GetAllAnimals
{
    public record GetAllAnimalsQuery : IRequest<List<AnimalInfoResponseDto>>;
}
