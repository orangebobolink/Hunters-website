using Modules.Document.Application.Dtos.RequestDtos;
using Modules.Document.Application.Dtos.ResponseDtos;

namespace Modules.Document.Application.Interfaces
{
    public interface IPermissionService
    {
        Task<PermissionResponseDto> CreateAsync(PermisionRequestDto request, CancellationToken cancellationToken);
        Task<PermissionResponseDto> UpdateAsync(Guid id, PermisionRequestDto request, CancellationToken cancellationToken);
        Task<PermissionResponseDto> DeleteAsync(Guid id, CancellationToken cancellationToken);
        Task<PermissionResponseDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<List<PermissionResponseDto>> GetAllAsync(CancellationToken cancellationToken);
        public Task<PermissionResponseDto> GetByIdIncludeAsync(Guid id, CancellationToken cancellationToken);
        public Task<List<PermissionResponseDto>> GetAllIncludeAsync(CancellationToken cancellationToken);
    }
}
