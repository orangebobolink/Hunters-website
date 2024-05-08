using Microsoft.AspNetCore.Mvc;

namespace Payment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        [HttpPost("{licenseNumber}")]
        public async Task<IActionResult> PayForHuntingLicenseFee(
            [FromRoute] string licenseNumber,
            CancellationToken cancellationToken = default)
        {

        }
    }
}
