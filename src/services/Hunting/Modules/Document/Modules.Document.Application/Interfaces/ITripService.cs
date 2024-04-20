using Modules.Document.Application.Dtos.RequestDtos;
using Modules.Document.Application.Dtos.ResponseDtos;

namespace Modules.Document.Application.Interfaces
{
    public interface ITripService
    {
        Task<TripResponseDto> CreateAsync(TripRequestDto request, CancellationToken cancellationToken);
        Task<TripResponseDto> UpdateAsync(Guid id, TripRequestDto request, CancellationToken cancellationToken);
        Task<TripResponseDto> DeleteAsync(Guid id, CancellationToken cancellationToken);
        Task<TripResponseDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<List<TripResponseDto>> GetAllAsync(CancellationToken cancellationToken);
        public Task<TripResponseDto> GetByIdIncludeAsync(Guid id, CancellationToken cancellationToken);
        public Task<List<TripResponseDto>> GetAllIncludeAsync(CancellationToken cancellationToken);
    }
}
