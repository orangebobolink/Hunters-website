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

        [HttpPost]
        public async Task<IActionResult> Create(TripRequestDto request, CancellationToken cancellationToken = default)
        {
            var createdTripParticipant = await _tripParticipantService.CreateAsync(request, cancellationToken);

            return Ok(createdTripParticipant);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, TripRequestDto request, CancellationToken cancellationToken = default)
        {
            var updatedTripParticipant = await _tripParticipantService.UpdateAsync(id, request, cancellationToken);

            return Ok(updatedTripParticipant);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken = default)
        {
            var deletedTripParticipant = await _tripParticipantService.DeleteAsync(id, cancellationToken);

            return Ok(deletedTripParticipant);
        }
    }
}
