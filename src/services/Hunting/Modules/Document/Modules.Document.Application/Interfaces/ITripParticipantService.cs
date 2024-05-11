using Modules.Document.Application.Dtos.RequestDtos;
using Modules.Document.Application.Dtos.ResponseDtos;

namespace Modules.Document.Application.Interfaces
{
    public interface ITripParticipantService
    {
        Task<TripParticipantResponseDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<TripParticipantResponseDto> CreateAsync(TripParticipantRequestDto request, CancellationToken cancellationToken);
        Task<TripParticipantResponseDto> UpdateAsync(Guid id, TripParticipantRequestDto request, CancellationToken cancellationToken);
        Task<TripParticipantResponseDto> DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
