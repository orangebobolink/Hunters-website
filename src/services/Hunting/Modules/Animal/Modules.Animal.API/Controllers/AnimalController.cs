using MediatR;
using Microsoft.AspNetCore.Mvc;
using Modules.Animal.Application.Features.Animal.Queries.GetAllAnimals;

namespace Modules.Animal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimalController(IMediator mediator)
                : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet]
        public async Task<IActionResult> GetAllAnimals(CancellationToken cancellationToken = default)
        {
            var query = new GetAllAnimalsQuery();
            var result = await _mediator.Send(query, cancellationToken);

            return Ok(result);
        }
    }
}
