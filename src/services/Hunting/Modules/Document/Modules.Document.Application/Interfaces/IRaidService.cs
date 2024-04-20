using Modules.Document.Application.Dtos.RequestDtos;
using Modules.Document.Application.Dtos.ResponseDtos;

namespace Modules.Document.Application.Interfaces
{
    public interface IRaidService
    {
        Task<RaidResponseDto> CreateAsync(RaidRequestDto request, CancellationToken cancellationToken);
        Task<RaidResponseDto> UpdateAsync(Guid id, RaidRequestDto request, CancellationToken cancellationToken);
        Task<RaidResponseDto> DeleteAsync(Guid id, CancellationToken cancellationToken);
        Task<RaidResponseDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<List<RaidResponseDto>> GetAllAsync(CancellationToken cancellationToken);
        public Task<RaidResponseDto> GetByIdIncludeAsync(Guid id, CancellationToken cancellationToken);
        public Task<List<RaidResponseDto>> GetAllIncludeAsync(CancellationToken cancellationToken);
    }
}
