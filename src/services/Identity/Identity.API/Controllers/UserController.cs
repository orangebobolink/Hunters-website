using Identity.Domain.Entities;
using Identity.Services.Dtos.RequestDtos;
using Identity.Services.Dtos.ResponseDtos;
using Identity.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MR.AspNetCore.Pagination;

namespace Identity.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IUserService userService) : ControllerBase
    {
        private readonly IUserService _userService = userService;

        [Authorize(Roles = Role.Admin)]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<KeysetPaginationResult<ResponseUserDto>>> GetAllUsersAsync(CancellationToken cancellationToken = default)
        {
            var users = await _userService.GetAllAsync(cancellationToken);

            return Ok(users);
        }

        [Authorize]
        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<ResponseUserDto>> GetUserByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var user = await _userService.GetUserByIdAsync(id, cancellationToken);

            return Ok(user);
        }

        [Authorize(Roles = Role.Admin)]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ResponseCreateUserDto>> CreateUserAsync([FromBody] RequestUserDto requestUserDto,
                                                                                CancellationToken cancellationToken = default)
        {
            var user = await _userService.CreateAsync(requestUserDto, cancellationToken);

            return Ok(user);
        }

        [Authorize]
        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ResponseUpdateUserDto>> UpdateUserAsync([FromQuery] Guid id,
                                                                         [FromBody] RequestUserDto requestUserDto,
                                                                         CancellationToken cancellationToken = default)
        {
            var user = await _userService.UpdateAsync(id, requestUserDto, cancellationToken);

            return Ok(user);
        }
    }
}
