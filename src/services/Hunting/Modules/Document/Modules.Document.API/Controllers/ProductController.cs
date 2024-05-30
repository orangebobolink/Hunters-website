using Microsoft.AspNetCore.Mvc;
using Modules.Document.Application.Interfaces;

namespace Modules.Document.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(
        IProductService productService)
        : ControllerBase
    {
        private readonly IProductService _productService = productService;

        [HttpGet]
        public async Task<IActionResult> GetAllProducts(CancellationToken cancellationToken = default)
        {
            var products = await _productService.GetAllAsync(cancellationToken);

            return Ok(products);
        }
    }
}
