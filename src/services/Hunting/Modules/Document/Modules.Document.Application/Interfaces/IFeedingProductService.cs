using Modules.Document.Application.Dtos.RequestDtos;
using Modules.Document.Application.Dtos.ResponseDto;

namespace Modules.Document.Application.Interfaces
{
    public interface IFeedingProductService
    {
        Task<FeedingProductResponseDto> UpdateAsync(Guid id, FeedingProductRequestDto request, CancellationToken cancellationToken);
        Task<FeedingProductResponseDto> DeleteAsync(Guid id, CancellationToken cancellationToken);
        Task<FeedingProductResponseDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<List<FeedingProductResponseDto>> GetAllAsync(CancellationToken cancellationToken);
    }
}
