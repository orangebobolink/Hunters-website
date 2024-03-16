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
        public async Task<IActionResult> RefreshAsync(CancellationToken cancellationToken = default)
        {
            var response = await _tokenService.RefreshAsync(cancellationToken);

            return Ok(response);
        }

        [Authorize]
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Revoke(CancellationToken cancellationToken = default)
        {
            await _tokenService.RevokeAsync(cancellationToken);

            return Ok();
        }
    }
}
