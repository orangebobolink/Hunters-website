using Modules.Document.Application.Dtos.RequestDtos;
using Modules.Document.Application.Dtos.ResponseDto;

namespace Modules.Document.Application.Interfaces
{
    internal interface IPermissionService
    {
        Task<PermisionResponseDto> CreateAsync(PermisionRequestDto request, CancellationToken cancellationToken);
        Task<PermisionResponseDto> UpdateAsync(Guid id, PermisionRequestDto request, CancellationToken cancellationToken);
        Task<PermisionResponseDto> DeleteAsync(Guid id, CancellationToken cancellationToken);
        Task<PermisionResponseDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<List<PermisionResponseDto>> GetAllAsync(CancellationToken cancellationToken);
        public Task<PermisionResponseDto?> GetByIdIncludeAsync(Guid id, CancellationToken cancellationToken);
        public Task<List<PermisionResponseDto>> GetAllIncludeAsync(CancellationToken cancellationToken);
    }
}
