﻿using Identity.Services.Dtos.RequestDtos;
using Identity.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Identity.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private IAuthorizationService _authorizationService;

        public AuthorizationController(IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] RequestLoginUserDto loginUserDto,
                                                    CancellationToken cancellationToken = default)
        {
            var response = await _authorizationService.LoginAsync(loginUserDto, cancellationToken);

            return Ok(response);
        }

        [HttpPost("registration")]
        public async Task<IActionResult> RegistrationAsync([FromBody] RequestRegistrationUserDto registrationUserDto,
                                                            CancellationToken cancellationToken = default)
        {
            var response = await _authorizationService.RegistrationAsync(registrationUserDto, cancellationToken);

            return Ok(response);
        }
    }
}
