using Identity.Domain.Entities;
using Identity.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Identity.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HuntingController(
        IHuntingLicenseService huntingLicenseService)
        : ControllerBase
    {
        private readonly IHuntingLicenseService _huntingLicenseService = huntingLicenseService;

        [HttpGet("{licenseNumber}")]
        public async Task<ActionResult<HuntingLicense>> GetHunterLicenseByLicenseNumber(
            string licenseNumber,
            CancellationToken cancellationToken = default)
        {
            var respons = await _huntingLicenseService
                .GetByLicenseNumberAsync(licenseNumber, cancellationToken);

            return Ok(respons);
        }
    }
}
