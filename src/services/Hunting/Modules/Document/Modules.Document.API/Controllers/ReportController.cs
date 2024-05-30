using Microsoft.AspNetCore.Mvc;
using Modules.Document.Application.Interfaces;
using Modules.Document.Domain.Enums;

namespace Modules.Document.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController(
        IReportingService reportService)
        : ControllerBase
    {
        private readonly IReportingService _reportService = reportService;

        [HttpGet("animals-by-popular/{period}")]
        public async Task<IActionResult> GetAnimalsByPopular(
            [FromRoute] PeriodReport period,
            CancellationToken cancellationToken = default)
        {
            var response = await _reportService.GetAnimalsByPopular(
                period,
                cancellationToken);

            return Ok(response);
        }
    }
}
