namespace Modules.Document.Domain.Interfaces
{
    public interface IRepository<T>
       where T : IBaseEntity
    {
        public Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        public Task<List<T>> GetAllAsync(CancellationToken cancellationToken);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}
