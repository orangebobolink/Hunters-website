using Microsoft.AspNetCore.Mvc;
using Modules.Document.Application.Dtos.RequestDtos;
using Modules.Document.Application.Interfaces;

namespace Modules.Document.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LandController(ILandService landService) : ControllerBase
    {
        private readonly ILandService _landService = landService;

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
        {
            var lands = await _landService.GetAllAsync(cancellationToken);
            return Ok(lands);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken = default)
        {
            var land = await _landService.GetByIdAsync(id, cancellationToken);

            return Ok(land);
        }

        [HttpPost]
        public async Task<IActionResult> Create(LandRequestDto request, CancellationToken cancellationToken = default)
        {
            var createdLand = await _landService.CreateAsync(request, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = createdLand.Id }, createdLand);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, LandRequestDto request, CancellationToken cancellationToken = default)
        {
            var updatedLand = await _landService.UpdateAsync(id, request, cancellationToken);
            return Ok(updatedLand);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken = default)
        {
            var deletedLand = await _landService.DeleteAsync(id, cancellationToken);
            return Ok(deletedLand);
        }
    }
}
