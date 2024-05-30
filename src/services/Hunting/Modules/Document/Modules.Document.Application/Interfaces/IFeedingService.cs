using Modules.Document.Application.Dtos.RequestDtos;
using Modules.Document.Application.Dtos.ResponseDtos;

namespace Modules.Document.Application.Interfaces
{
    public interface IFeedingService
    {
        Task<FeedingResponseDto> CreateAsync(FeedingRequestDto request, CancellationToken cancellationToken);
        Task<FeedingResponseDto> UpdateAsync(Guid id, FeedingRequestDto request, CancellationToken cancellationToken);
        Task<FeedingResponseDto> DeleteAsync(Guid id, CancellationToken cancellationToken);
        Task<FeedingResponseDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<List<FeedingResponseDto>> GetAllAsync(CancellationToken cancellationToken);
        public Task<FeedingResponseDto> GetByIdIncludeAsync(Guid id, CancellationToken cancellationToken);
        public Task<List<FeedingResponseDto>> GetAllIncludeAsync(CancellationToken cancellationToken);
    }
}
