using Identity.Domain.Entities;
using Identity.Services.Dtos.ResponseDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Identity.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VerificationController : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<HuntingLicense>> VerifyHunterLicense(
            string licanseNumber,
            CancellationToken cancellationToken = default)
        {

        }
    }
}