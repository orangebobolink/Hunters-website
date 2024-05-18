using Modules.Document.Application.Dtos.ResponseDtos;

namespace Modules.Document.Application.Interfaces
{
    public interface ICouponService
    {
        Task<CouponResponseDto> UpdateIsUsedAsync(Guid id, CancellationToken cancellationToken);
        Task<CouponResponseDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<List<CouponResponseDto>> GetAllAsync(CancellationToken cancellationToken);
        Task<List<CouponResponseDto>> GetAllOnlyIsNotUsedAsync(CancellationToken cancellationToken);
    }
}
