using Identity.Domain.Entities;
using Identity.Services.Dtos.RequestDtos;
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

        [HttpGet("user/{id}")]
        public async Task<ActionResult<HuntingLicense>> GetByUserId(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            var respons = await _huntingLicenseService
                .GetByUserIdAsync(id, cancellationToken);

            return Ok(respons);
        }

        [HttpPost]
        public async Task<ActionResult<HuntingLicense>> Create(
            HuntingLicenseRequestDto huntingLicenseRequestDto,
            CancellationToken cancellationToken = default)
        {
            var respons = await _huntingLicenseService
                .CreateAsync(huntingLicenseRequestDto, cancellationToken);

            return Ok(respons);
        }
    }
}
