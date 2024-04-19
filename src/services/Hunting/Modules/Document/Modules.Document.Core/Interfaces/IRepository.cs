using System.Linq.Expressions;

namespace Modules.Document.Domain.Interfaces
{
    public interface IRepository<T>
       where T : IBaseEntity
    {
        Task<T?> GetByPredicate(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken);
        Task<IEnumerable<T>> GetAllByPredicate(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken);
        Task<List<T>> GetAllAsync(CancellationToken cancellationToken);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}
