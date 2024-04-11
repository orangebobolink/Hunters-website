using MediatR;
using Modules.Animal.Application.Dtos.ResponseDtos;

namespace Modules.Animal.Application.Features.Animal.Queries.GetAnimalById
{
    public record class GetAnimalByIdQuery(Guid Id) : IRequest<AnimalInfoResponseDto>;
}
