using Microsoft.AspNetCore.Mvc;
using Modules.Document.Application.Dtos.RequestDtos;
using Modules.Document.Application.Interfaces;

namespace Modules.Document.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedingProductController(
        IFeedingProductService feedingProductService)
        : ControllerBase
    {
        private readonly IFeedingProductService _feedingProductService = feedingProductService;

        [HttpGet]
        public async Task<IActionResult> GetAllFeedingProducts(CancellationToken cancellationToken = default)
        {
            var feedingProducts = await _feedingProductService.GetAllAsync(cancellationToken);

            return Ok(feedingProducts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFeedingProductById(Guid id, CancellationToken cancellationToken = default)
        {
            var feedingProduct = await _feedingProductService.GetByIdAsync(id, cancellationToken);

            return Ok(feedingProduct);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFeedingProduct(Guid id, FeedingProductRequestDto request, CancellationToken cancellationToken = default)
        {
            var updatedFeedingProduct = await _feedingProductService
                .UpdateAsync(id, request, cancellationToken);

            return Ok(updatedFeedingProduct);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFeedingProduct(Guid id, CancellationToken cancellationToken = default)
        {
            var deletedFeedingProduct = await _feedingProductService.DeleteAsync(id, cancellationToken);

            return Ok(deletedFeedingProduct);
        }
    }
}
