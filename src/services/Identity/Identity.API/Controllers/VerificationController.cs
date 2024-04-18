using Identity.Domain.Entities;
using Identity.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Identity.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VerificationController(IVerificationService verificationService) : ControllerBase
    {
        private readonly IVerificationService _verificationService = verificationService;

        [HttpPost]
        public async Task<ActionResult<HuntingLicense>> VerifyHunterLicense(
            string licanseNumber,
            CancellationToken cancellationToken = default)
        {
            var response = await _verificationService.VerifyHuntingLicenseAsync(licanseNumber, cancellationToken);

            return Ok(response);
        }
    }
}