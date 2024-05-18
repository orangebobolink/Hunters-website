using Microsoft.AspNetCore.Mvc;
using Modules.Document.Application.Dtos.RequestDtos;
using Modules.Document.Application.Interfaces;

namespace Modules.Document.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedingController(
        IFeedingService feedingService)
        : ControllerBase
    {
        private readonly IFeedingService _feedingService = feedingService;

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
        {
            var feedings = await _feedingService.GetAllAsync(cancellationToken);

            return Ok(feedings);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken = default)
        {
            var feeding = await _feedingService.GetByIdAsync(id, cancellationToken);

            return Ok(feeding);
        }

        [HttpGet("{id}/include")]
        public async Task<IActionResult> GetByIdInclude(Guid id, CancellationToken cancellationToken = default)
        {
            var feeding = await _feedingService.GetByIdIncludeAsync(id, cancellationToken);

            return Ok(feeding);
        }

        [HttpGet("include")]
        public async Task<IActionResult> GetAllInclude(CancellationToken cancellationToken = default)
        {
            var feedings = await _feedingService.GetAllIncludeAsync(cancellationToken);

            return Ok(feedings);
        }

        [HttpPost]
        public async Task<IActionResult> Create(FeedingRequestDto request, CancellationToken cancellationToken = default)
        {
            var createdFeeding = await _feedingService.CreateAsync(request, cancellationToken);

            return Ok(createdFeeding);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, FeedingRequestDto request, CancellationToken cancellationToken = default)
        {
            var updatedFeeding = await _feedingService.UpdateAsync(id, request, cancellationToken);

            return Ok(updatedFeeding);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken = default)
        {
            var deletedFeeding = await _feedingService.DeleteAsync(id, cancellationToken);

            return Ok(deletedFeeding);
        }
    }
}
