using Microsoft.AspNetCore.Mvc;
using Rent.Application.Dtos.RequestDtos;
using Rent.Application.Interfaces;

namespace Rent.API.Controllers
{
    [Route("api/rent/[controller]")]
    [ApiController]
    public class ProductController(
        IProductService productService)
        : ControllerBase
    {
        private readonly IProductService _productService = productService;

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            var response = await _productService.GetByIdAsync(id, cancellationToken);

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(
            CancellationToken cancellationToken = default)
        {
            var response = await _productService.GetAllAsync(cancellationToken);

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            ProductRequestDto request,
            CancellationToken cancellationToken = default)
        {
            var response = await _productService.CreateAsync(
                request,
                cancellationToken);

            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            Guid id,
            ProductRequestDto request,
            CancellationToken cancellationToken = default)
        {
            var response = await _productService.UpdateAsync(
                id,
                request,
                cancellationToken);

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            var response = await _productService.DeleteAsync(
                id,
                cancellationToken);

            return Ok(response);
        }
    }
}
