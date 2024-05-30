using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Modules.Animal.Application.Dtos.RequestDtos;
using Modules.Animal.Application.Features.AnimalFeatures.Commands.AnimalCreate;
using Modules.Animal.Application.Features.AnimalFeatures.Commands.AnimalDelete;
using Modules.Animal.Application.Features.AnimalFeatures.Commands.AnimalUpdate;
using Modules.Animal.Application.Features.AnimalFeatures.Queries.GetAllAnimalsWithFullInformation;
using Modules.Animal.Application.Features.AnimalFeatures.Queries.GetAnimalById;

namespace Modules.Animal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimalController(
        IMediator mediator)
        : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet]
        public async Task<IActionResult> GetAllAnimals(
            CancellationToken cancellationToken = default)
        {
            var query = new GetAllAnimalsWithFullInformationQuery();
            var result = await _mediator.Send(query, cancellationToken);

            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetAnimalById(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            var query = new GetAnimalByIdQuery(id);
            var result = await _mediator.Send(query, cancellationToken);

            return Ok(result);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateAnimal(
            AnimalInfoRequestDto request,
            CancellationToken cancellationToken = default)
        {
            var command = new AnimalCreateCommand(request);
            var result = await _mediator.Send(command, cancellationToken);

            return Ok(result);
        }

        [Authorize]
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateAnimal(
            Guid id,
            AnimalInfoRequestDto request,
            CancellationToken cancellationToken = default)
        {
            var command = new AnimalUpdateCommand(id, request);
            var result = await _mediator.Send(command, cancellationToken);

            return Ok(result);
        }

        [Authorize]
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteAnimal(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            var command = new AnimalDeleteCommand(id);
            await _mediator.Send(command, cancellationToken);

            return Ok();
        }
    }
}
