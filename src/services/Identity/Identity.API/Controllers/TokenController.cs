using Identity.Services.Dtos;
using Identity.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Identity.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController(ITokenService tokenService) : ControllerBase
    {
        private readonly ITokenService _tokenService = tokenService;

        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshAsync([FromQuery] Guid id,
                                                    [FromBody] TokenApiDto tokenApiDto,
                                                    CancellationToken cancellationToken = default)
        {
            var response = await _tokenService.Refresh(id, tokenApiDto, cancellationToken);

            return Ok(response);
        }

        [Authorize]
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Revoke([FromQuery] Guid id,
                                                CancellationToken cancellationToken = default)
        {
            await _tokenService.Revoke(id, cancellationToken);

            return Ok();
        }
    }
}
