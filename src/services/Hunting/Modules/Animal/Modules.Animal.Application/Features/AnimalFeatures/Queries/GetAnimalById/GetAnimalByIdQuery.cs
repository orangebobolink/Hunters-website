using MediatR;
using Modules.Animal.Application.Dtos.ResponseDtos;

namespace Modules.Animal.Application.Features.AnimalFeatures.Queries.GetAnimalById
{
    public record GetAnimalByIdQuery(Guid Id) : IRequest<AnimalInfoResponseDto>;
}
