using Modules.Document.Application.Dtos.RequestDtos;
using Modules.Document.Application.Dtos.ResponseDtos;

namespace Modules.Document.Application.Interfaces
{
    public interface ITripParticipantService
    {
        Task<TripResponseDto> CreateAsync(TripRequestDto request, CancellationToken cancellationToken);
        Task<TripResponseDto> UpdateAsync(Guid id, TripRequestDto request, CancellationToken cancellationToken);
        Task<TripResponseDto> DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
