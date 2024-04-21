using Identity.Domain.Entities;
using Identity.Services.Dtos.RequestDtos;
using Identity.Services.Dtos.ResponseDtos;
using Identity.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MR.EntityFrameworkCore.KeysetPagination;

namespace Identity.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IUserService userService) : ControllerBase
    {
        private readonly IUserService _userService = userService;

        [Authorize(Roles = Role.Admin)]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<ResponseUserDto>>> GetAllUsers(
            [FromQuery] Guid id,
            [FromQuery] int numberTake = 10,
            [FromQuery] KeysetPaginationDirection keysetPaginationDirection = KeysetPaginationDirection.Forward,
            CancellationToken cancellationToken = default)
        {
            var users = await _userService.GetAllAsync(id, numberTake, keysetPaginationDirection, cancellationToken);

            return Ok(users);
        }

        [Authorize]
        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ResponseUserDto>> GetUserById(Guid id,
            CancellationToken cancellationToken = default)
        {
            var user = await _userService.GetUserByIdAsync(id, cancellationToken);

            return Ok(user);
        }

        [Authorize(Roles = $"{Role.Manager}, {Role.Admin}")]
        [HttpGet("{roleName}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<ResponseUserDto>>> GetAllByRoles(string roleName,
            CancellationToken cancellationToken = default)
        {
            var user = await _userService.GetALlByRoles(roleName, cancellationToken);

            return Ok(user);
        }

        [Authorize(Roles = Role.Admin)]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ResponseCreateUserDto>> CreateUser([FromBody] RequestUserDto requestUserDto,
                                                                                CancellationToken cancellationToken = default)
        {
            var user = await _userService.CreateAsync(requestUserDto, cancellationToken);

            return Ok(user);
        }

        [Authorize]
        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ResponseUpdateUserDto>> UpdateUser([FromRoute] Guid id,
                                                                         [FromBody] RequestUpdateUserDto requestUserDto,
                                                                         CancellationToken cancellationToken = default)
        {
            var user = await _userService.UpdateAsync(id, requestUserDto, cancellationToken);

            return Ok(user);
        }
    }
}
