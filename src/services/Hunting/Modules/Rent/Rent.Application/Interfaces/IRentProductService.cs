using Rent.Application.Dtos.RequestDtos;
using Rent.Application.Dtos.ResponseDtos;

namespace Rent.Application.Interfaces
{
    public interface IRentProductService
    {
        Task<RentProductResponseDto> GetByIdAsync(
           Guid id,
           CancellationToken cancellationToken);
        Task<IEnumerable<RentProductResponseDto>> GetAllAsync(
            CancellationToken cancellationToken);
        Task<RentProductResponseDto> CreateAsync(
             RentProductRequestDto request,
             CancellationToken cancellationToken);
        Task<RentProductResponseDto> UpdateAsync(
            Guid id,
            RentProductRequestDto request,
            CancellationToken cancellationToken);
        Task<RentProductResponseDto> DeleteAsync(
            Guid id,
            CancellationToken cancellationToken);
    }
}
