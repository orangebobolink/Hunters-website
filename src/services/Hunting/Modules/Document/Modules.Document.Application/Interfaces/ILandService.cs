using Modules.Document.Application.Dtos.RequestDtos;
using Modules.Document.Application.Dtos.ResponseDto;

namespace Modules.Document.Application.Interfaces
{
    public interface ILandService
    {
        Task<LandResponseDto> CreateAsync(LandRequestDto request, CancellationToken cancellationToken);
        Task<LandResponseDto> UpdateAsync(Guid id, LandRequestDto request, CancellationToken cancellationToken);
        Task<LandResponseDto> DeleteAsync(Guid id, CancellationToken cancellationToken);
        Task<LandResponseDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<List<LandResponseDto>> GetAllAsync(CancellationToken cancellationToken);
    }
}
