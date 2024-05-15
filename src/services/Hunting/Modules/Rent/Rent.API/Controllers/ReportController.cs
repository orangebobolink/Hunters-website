using Microsoft.AspNetCore.Mvc;
using Rent.Application.Interfaces;
using Rent.Domain.Enums;

namespace Rent.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController(
        IReportService reportService)
        : ControllerBase
    {
        private readonly IReportService _reportService = reportService;

        [HttpGet("products-by-popular/{period}")]
        public async Task<IActionResult> GetProductsByPopular(
            [FromRoute] Period period,
            CancellationToken cancellationToken = default)
        {
            var response = await _reportService.GetProductsByPopularAsync(
                period,
                cancellationToken);

            return Ok(response);
        }

        [HttpGet("products-revenue/{period}")]
        public async Task<IActionResult> GetProductsRevenueByPeriod(
            [FromRoute] Period period,
            CancellationToken cancellationToken = default)
        {
            var response = await _reportService.GetProductsRevenueByPeriodAsync(
                period,
                cancellationToken);

            return Ok(response);
        }
    }
}
