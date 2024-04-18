using Identity.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Identity.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HuntingController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<HuntingLicense>> GetHunterLicense(
            Guid id,
            CancellationToken cancellationToken = default)
        {

        }
    }
}
