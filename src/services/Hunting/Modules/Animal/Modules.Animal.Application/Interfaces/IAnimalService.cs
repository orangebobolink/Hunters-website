using Modules.Animal.Application.Dtos.RequestDtos;
using Modules.Animal.Application.Dtos.ResponseDtos;
using Modules.Animal.Domain.Entities;

namespace Modules.Animal.Application.Interfaces
{
    public interface IAnimalService
    {
        Task<AnimalInfoResponseDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<List<AnimalInfoResponseDto>> GetAllAsync(CancellationToken cancellationToken);
        Task<List<AnimalInfoResponseDto>> GetAllWithFullInformationAsync(CancellationToken cancellationToken);
        Task<AnimalInfoResponseDto> CreateAsync(AnimalInfoRequestDto requestAnimal, CancellationToken cancellationToken);
        Task<AnimalInfoResponseDto> UpdateAsync(Guid id, AnimalInfoRequestDto requestAnimal, CancellationToken cancellationToken);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
