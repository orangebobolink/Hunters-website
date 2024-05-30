using Microsoft.AspNetCore.Mvc;
using Modules.Document.Application.Dtos.RequestDtos;
using Modules.Document.Application.Interfaces;

namespace Modules.Document.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripParticipantController(
        ITripParticipantService tripParticipantService)
        : ControllerBase
    {
        private readonly ITripParticipantService _tripParticipantService = tripParticipantService;

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetTripParticipantById(Guid id, CancellationToken cancellationToken = default)
        {
            var trip = await _tripParticipantService.GetByIdAsync(id, cancellationToken);

            return Ok(trip);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTripParticipant(
            TripParticipantRequestDto request,
            CancellationToken cancellationToken = default)
        {
            var createdTripParticipant = await _tripParticipantService.CreateAsync(
                request,
                cancellationToken);

            return Ok(createdTripParticipant);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTripParticipant(
            Guid id,
            TripParticipantRequestDto request,
            CancellationToken cancellationToken = default)
        {
            var updatedTripParticipant = await _tripParticipantService.UpdateAsync(id, request, cancellationToken);

            return Ok(updatedTripParticipant);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTripParticipant(Guid id, CancellationToken cancellationToken = default)
        {
            var deletedTripParticipant = await _tripParticipantService.DeleteAsync(id, cancellationToken);

            return Ok(deletedTripParticipant);
        }
    }
}
