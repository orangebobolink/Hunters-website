using DrfLikePaginations;
using Identity.Domain.Entities;
using Identity.Services.Dtos.RequestDtos;
using Identity.Services.Dtos.ResponseDtos;
using Identity.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MR.AspNetCore.Pagination;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Identity.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;
        private readonly UserManager<User> _userManager;
        public UserController(IUserService userService, UserManager<User> userManager)
        {
            _userService = userService;
            _userManager = userManager;
        }

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
