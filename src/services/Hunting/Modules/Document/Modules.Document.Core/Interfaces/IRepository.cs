using System.Linq.Expressions;

namespace Modules.Document.Domain.Interfaces
{
    public interface IRepository<T>
       where T : IBaseEntity
    {
        Task<T?> GetByPredicate(
             Expression<Func<T, bool>> predicate,
             CancellationToken cancellationToken = default,
             bool exceptIncludes = false,
             bool trackChanges = true);
        Task<IEnumerable<T>> GetAllByPredicate(
            Expression<Func<T, bool>> predicate,
           CancellationToken cancellationToken = default,
            bool exceptIncludes = false,
            bool trackChanges = true);
        Task<List<T>> GetAllAsync(
            CancellationToken cancellationToken = default,
            bool exceptIncludes = false,
            bool trackChanges = true);
        void Create(T entity);
        Task CreateRange(List<T> entity);
        void Update(T entity);
        void Delete(T entity);
        Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}
