using Microsoft.AspNetCore.Mvc;
using Modules.Document.Application.Dtos.RequestDtos;
using Modules.Document.Application.Interfaces;

namespace Modules.Document.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RaidController(IRaidService raidService) : ControllerBase
    {
        private readonly IRaidService _raidService = raidService;

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
        {
            var raids = await _raidService.GetAllAsync(cancellationToken);

            return Ok(raids);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRaidsById(Guid id, CancellationToken cancellationToken = default)
        {
            var raid = await _raidService.GetRaidsByIdAsync(id, cancellationToken);

            return Ok(raid);
        }

        [HttpGet("{id}/include")]
        public async Task<IActionResult> GetByIdInclude(Guid id, CancellationToken cancellationToken = default)
        {
            var raid = await _raidService.GetByIdIncludeAsync(id, cancellationToken);

            return Ok(raid);
        }

        [HttpGet("include")]
        public async Task<IActionResult> GetAllInclude(CancellationToken cancellationToken = default)
        {
            var raids = await _raidService.GetAllIncludeAsync(cancellationToken);

            return Ok(raids);
        }

        [HttpPost]
        public async Task<IActionResult> Create(RaidRequestDto request, CancellationToken cancellationToken = default)
        {
            var createdRaid = await _raidService.CreateAsync(request, cancellationToken);

            return Ok(createdRaid);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, RaidRequestDto request, CancellationToken cancellationToken = default)
        {
            var updatedRaid = await _raidService.UpdateAsync(id, request, cancellationToken);

            return Ok(updatedRaid);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken = default)
        {
            var deletedRaid = await _raidService.DeleteAsync(id, cancellationToken);

            return Ok(deletedRaid);
        }
    }
}
