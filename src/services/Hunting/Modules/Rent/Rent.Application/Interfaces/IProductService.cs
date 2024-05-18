using Rent.Application.Dtos.RequestDtos;
using Rent.Application.Dtos.ResponseDtos;

namespace Rent.Application.Interfaces
{
    public interface IProductService
    {
        Task<ProductResponseDto> CreateAsync(
            ProductRequestDto request,
            CancellationToken cancellationToken);
        Task<ProductResponseDto> UpdateAsync(
            Guid id,
            ProductRequestDto request,
            CancellationToken cancellationToken);
        Task<ProductResponseDto> DeleteAsync(
            Guid id,
            CancellationToken cancellationToken);
        Task<ProductResponseDto> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken);
        Task<IEnumerable<ProductResponseDto>> GetByType(
            string type,
            CancellationToken cancellationToken);
        Task<IEnumerable<ProductResponseDto>> GetAllAsync(
            CancellationToken cancellationToken);
    }
}
