﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Modules.Animal.Application.Dtos.RequestDtos;
using Modules.Animal.Application.Features.HuntingSeasonFeatures.Commands.HuntingSeasonCreate;
using Modules.Animal.Application.Features.HuntingSeasonFeatures.Commands.HuntingSeasonDelete;
using Modules.Animal.Application.Features.HuntingSeasonFeatures.Queries.GetAllHutningSeasons;

namespace Modules.Animal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HuntingSeasonController(
        IMediator mediator)
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

        [Authorize]
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteHuntingSeason(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            var command = new HuntingSeasonDeleteCommand(id);
            var result = await _mediator.Send(command, cancellationToken);

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllHuntingSeasons(
            CancellationToken cancellationToken = default)
        {
            var query = new GetAllHutningSeasonsQuery();
            var result = await _mediator.Send(query, cancellationToken);

            return Ok(result);
        }
    }
}
