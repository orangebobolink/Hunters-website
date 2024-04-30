using Modules.Animal.Domain.Interfaces.Repositories;
using Modules.Animal.Infrastructure.Contexts;

namespace Modules.Animal.Infrastructure.Repositories
{
    internal abstract class BaseRepository<T>(ApplicationDbContext context)
        : IRepository<T>
        where T : class
    {
        protected readonly ApplicationDbContext _context = context;

        public void Create(T entity)
        {
            _context.Set<T>()
                .Add(entity);
        }

        public void Delete(T entity)
        {
            _context.Set<T>()
                .Remove(entity);
        }

        public void Update(T entity)
        {
            _context.Set<T>()
                .Update(entity);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }

        public abstract Task<List<T>> GetAllAsync(CancellationToken cancellationToken);

        public abstract Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
