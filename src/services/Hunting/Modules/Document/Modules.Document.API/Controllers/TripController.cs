using MassTransit.Mediator;
using Microsoft.AspNetCore.Mvc;
using Modules.Document.Application.Dtos.RequestDtos;
using Modules.Document.Application.Interfaces;

namespace Modules.Document.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripController(ITripService tripService) : ControllerBase
    {
        private readonly ITripService _tripService = tripService;

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
        {
            var result = await _tripService.GetAllAsync(cancellationToken);

            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken = default)
        {
            var trip = await _tripService.GetByIdAsync(id, cancellationToken);

            return Ok(trip);
        }

        [HttpGet("{id:guid}/include")]
        public async Task<IActionResult> GetByIdInclude(Guid id, CancellationToken cancellationToken = default)
        {
            var trip = await _tripService.GetByIdIncludeAsync(id, cancellationToken);

            return Ok(trip);
        }

        [HttpGet("include")]
        public async Task<IActionResult> GetAllInclude(CancellationToken cancellationToken = default)
        {
            var trips = await _tripService.GetAllIncludeAsync(cancellationToken);

            return Ok(trips);
        }

        [HttpPost]
        public async Task<IActionResult> Create(TripRequestDto tripDto, CancellationToken cancellationToken = default)
        {
            var createdTrip = await _tripService.CreateAsync(tripDto, cancellationToken);

            return Ok(createdTrip);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, TripRequestDto tripDto, CancellationToken cancellationToken = default)
        {
            var updatedTrip = await _tripService.UpdateAsync(id, tripDto, cancellationToken);

            return Ok(updatedTrip);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken = default)
        {
            var deletedTrip = await _tripService.DeleteAsync(id, cancellationToken);

            return Ok(deletedTrip);
        }
    }
}
