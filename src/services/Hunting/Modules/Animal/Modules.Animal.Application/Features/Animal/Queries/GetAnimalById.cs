using MediatR;
using Modules.Animal.Application.Dtos.ResponseDtos;

namespace Modules.Animal.Application.Features.Animal.Queries
{
    public record class GetAnimalById(Guid Id) : IRequest<AnimalInfoResponseDto>;
}
