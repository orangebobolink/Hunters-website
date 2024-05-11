using Microsoft.AspNetCore.Mvc;
using Payment.Application.Interfaces;

namespace Payment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController(
        IHuntingLicensePaymentService huntingLicensePaymentService,
        ITripPaymentService tripPaymentService)
        : ControllerBase
    {
        private readonly IHuntingLicensePaymentService _huntingLicensePaymentService
            = huntingLicensePaymentService;
        private readonly ITripPaymentService _tripPaymentService
            = tripPaymentService;

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

        [HttpPost("trip/{number}")]
        public async Task<IActionResult> PayForTrip(
            [FromRoute] string number,
            CancellationToken cancellationToken = default)
        {
            var response = await _tripPaymentService.TryToPayAsync(
                number,
                cancellationToken);

            return Ok(response);
        }
    }
}
