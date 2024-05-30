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
        public async Task<IActionResult> GetAllPermissions(CancellationToken cancellationToken = default)
        {
            var permissions = await _permissionService.GetAllAsync(cancellationToken);
            return Ok(permissions);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPermissionById(Guid id, CancellationToken cancellationToken = default)
        {
            var permission = await _permissionService.GetByIdAsync(id, cancellationToken);

            return Ok(permission);
        }

        [HttpGet("{id}/include")]
        public async Task<IActionResult> GetPermissionByIdInclude(Guid id, CancellationToken cancellationToken = default)
        {
            var permission = await _permissionService.GetByIdIncludeAsync(id, cancellationToken);

            return Ok(permission);
        }

        [HttpGet("include")]
        public async Task<IActionResult> GetAllPermissionsInclude(
            CancellationToken cancellationToken = default)
        {
            var permissions = await _permissionService.GetAllIncludeAsync(cancellationToken);

            return Ok(permissions);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePermission(
            PermisionRequestDto request,
            CancellationToken cancellationToken = default)
        {
            var createdPermission = await _permissionService.CreateAsync(request, cancellationToken);

            return Ok(createdPermission);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePermission(Guid id, PermisionRequestDto request, CancellationToken cancellationToken = default)
        {
            var updatedPermission = await _permissionService.UpdateAsync(id, request, cancellationToken);
            return Ok(updatedPermission);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePermission(Guid id, CancellationToken cancellationToken = default)
        {
            var deletedPermission = await _permissionService.DeleteAsync(id, cancellationToken);
            return Ok(deletedPermission);
        }
    }
}
