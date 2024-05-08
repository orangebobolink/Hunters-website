using Microsoft.AspNetCore.Mvc;
using Payment.Application.Interfaces;

namespace Payment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController(
        IHuntingLicensePaymentService huntingLicensePaymentService)
        : ControllerBase
    {
        private readonly IHuntingLicensePaymentService _huntingLicensePaymentService
            = huntingLicensePaymentService;

        [HttpPost("huntingLicense/{licenseNumber}")]
        public async Task<IActionResult> PayForHuntingLicenseFee(
            [FromRoute] string licenseNumber,
            CancellationToken cancellationToken = default)
        {
            var response = await _huntingLicensePaymentService.TryToPayAsync(
                licenseNumber,
                cancellationToken);

            return Ok(response);
        }

        [HttpPost("trip/{trip}")]
        public async Task<IActionResult> PayForTrip(
            [FromRoute] string number,
            CancellationToken cancellationToken = default)
        {
            var response = await _huntingLicensePaymentService.TryToPayAsync(
                licenseNumber,
                cancellationToken);

            return Ok(response);
        }
    }
}
