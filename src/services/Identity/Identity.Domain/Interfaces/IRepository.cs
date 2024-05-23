using System.Linq.Expressions;

namespace Identity.Domain.Interfaces
{
    public interface IRepository<T>
        where T : class
    {
        public Task<List<T>> GetAllAsync(CancellationToken cancellationToken);
        Task<T?> GetByPredicate(
            Expression<Func<T, bool>> predicate,
            CancellationToken cancellationToken);
        Task<IEnumerable<T>> GetAllByPredicate(
            Expression<Func<T, bool>> predicate,
            CancellationToken cancellationToken);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}
