using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Modules.Animal.Application.Dtos.RequestDtos;
using Modules.Animal.Application.Features.HuntingSeasonFeatures.Commands.HuntingSeasonCreate;

namespace Modules.Animal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HuntingSeasonController(IMediator mediator)
        : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateHuntingSeason(
            HuntingSeasonRequestDto request, 
            CancellationToken cancellationToken = default)
        {
            var command = new HuntingSeasonCreateCommand(request);
            var result = await _mediator.Send(command, cancellationToken);

            return Ok(result);
        }
    }
}
