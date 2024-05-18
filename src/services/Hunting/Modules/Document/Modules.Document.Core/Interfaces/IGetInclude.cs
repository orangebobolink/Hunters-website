namespace Modules.Document.Domain.Interfaces
{
    public interface IGetInclude<T>
        where T : IBaseEntity
    {
        public Task<T?> GetByIdIncludeAsync(Guid id, CancellationToken cancellationToken);
        public Task<List<T>> GetAllIncludeAsync(CancellationToken cancellationToken);
    }
}
