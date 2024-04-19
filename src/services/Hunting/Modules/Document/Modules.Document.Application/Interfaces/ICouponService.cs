using Modules.Document.Application.Dtos.ResponseDto;

namespace Modules.Document.Application.Interfaces
{
    public interface ICouponService
    {
        Task<CouponResponseDto> UpdateIsUsedAsync(Guid id);
        Task<CouponResponseDto?> GetByIdAsync(Guid id);
        Task<List<CouponResponseDto>> GetAllAsync();
        Task<List<CouponResponseDto>> GetAllOnlyIsNotUsedAsync();
    }
}
