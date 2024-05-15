using Microsoft.AspNetCore.Mvc;
using Rent.Application.Dtos.RequestDtos;
using Rent.Application.Interfaces;

namespace Rent.API.Controllers
{
    [Route("api/rent/[controller]")]
    [ApiController]
    public class RentProductController(
        IRentProductService rentProductService)
        : ControllerBase
    {
        private readonly IRentProductService _rentProductService = rentProductService;

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            var response = await _rentProductService.GetByIdAsync(id, cancellationToken);

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(
            CancellationToken cancellationToken = default)
        {
            var response = await _rentProductService.GetAllAsync(cancellationToken);

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create(
           RentProductRequestDto request,
           CancellationToken cancellationToken = default)
        {
            var response = await _rentProductService.CreateAsync(
                request,
                cancellationToken);

            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            Guid id,
            RentProductRequestDto request,
            CancellationToken cancellationToken = default)
        {
            var response = await _rentProductService.UpdateAsync(
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
            var response = await _rentProductService.DeleteAsync(
                id,
                cancellationToken);

            return Ok(response);
        }
    }
}
