using Microsoft.AspNetCore.Mvc;
using Rent.Application.Interfaces;

namespace Rent.API.Controllers
{
    [Route("api/rent/[controller]")]
    [ApiController]
    public class UserController(
        IUserService userService)
        : ControllerBase
    {
        private readonly IUserService _userService = userService;

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetUserById(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            var response = await _userService.GetByIdAsync(id, cancellationToken);

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers(
            CancellationToken cancellationToken = default)
        {
            var response = await _userService.GetAllAsync(cancellationToken);

            return Ok(response);
        }
    }
}
