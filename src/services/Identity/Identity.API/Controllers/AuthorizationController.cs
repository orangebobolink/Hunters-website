using Identity.Services.Dtos.RequestDtos;
using Identity.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Identity.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController(IAuthorizationService authorizationService) : ControllerBase
    {
        private readonly IAuthorizationService _authorizationService = authorizationService;

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> LoginAsync([FromBody] RequestLoginUserDto loginUserDto,
                                                    CancellationToken cancellationToken = default)
        {
            var response = await _authorizationService.LoginAsync(loginUserDto, cancellationToken);

            return Ok(response);
        }

        [HttpPost("registration")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RegistrationAsync([FromBody] RequestRegistrationUserDto registrationUserDto,
                                                            CancellationToken cancellationToken = default)
        {
            var response = await _authorizationService.RegistrationAsync(registrationUserDto, cancellationToken);

            return Ok(response);
        }
    }
}
