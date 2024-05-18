using Microsoft.AspNetCore.Mvc;
using Modules.Document.Application.Dtos.RequestDtos;
using Modules.Document.Application.Interfaces;

namespace Modules.Document.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionController(IPermissionService permissionService) : ControllerBase
    {
        private readonly IPermissionService _permissionService = permissionService;

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
        {
            var permissions = await _permissionService.GetAllAsync(cancellationToken);
            return Ok(permissions);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken = default)
        {
            var permission = await _permissionService.GetByIdAsync(id, cancellationToken);

            return Ok(permission);
        }

        [HttpGet("{id}/include")]
        public async Task<IActionResult> GetByIdInclude(Guid id, CancellationToken cancellationToken = default)
        {
            var permission = await _permissionService.GetByIdIncludeAsync(id, cancellationToken);

            return Ok(permission);
        }

        [HttpGet("include")]
        public async Task<IActionResult> GetAllInclude(CancellationToken cancellationToken = default)
        {
            var permissions = await _permissionService.GetAllIncludeAsync(cancellationToken);
            return Ok(permissions);
        }

        [HttpPost]
        public async Task<IActionResult> Create(PermisionRequestDto request, CancellationToken cancellationToken = default)
        {
            var createdPermission = await _permissionService.CreateAsync(request, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = createdPermission.Id }, createdPermission);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, PermisionRequestDto request, CancellationToken cancellationToken = default)
        {
            var updatedPermission = await _permissionService.UpdateAsync(id, request, cancellationToken);
            return Ok(updatedPermission);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken = default)
        {
            var deletedPermission = await _permissionService.DeleteAsync(id, cancellationToken);
            return Ok(deletedPermission);
        }
    }
}
