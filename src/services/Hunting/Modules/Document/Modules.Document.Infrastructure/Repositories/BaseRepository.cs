using Microsoft.EntityFrameworkCore;
using Modules.Document.Domain.Interfaces;
using Modules.Document.Infrastructure.Contexts;
using System.Linq.Expressions;

namespace Modules.Document.Infrastructure.Repositories
{
    internal class BaseRepository<T>(DocumentDbContext context) : IRepository<T>
        where T : class, IBaseEntity
    {
        protected readonly DocumentDbContext _context = context;

        public void Create(T entity)
        {
            _context.Set<T>()
                .Add(entity);
        }

        public async Task CreateRange(List<T> entity)
        {
            await _context.Set<T>()
                .AddRangeAsync(entity);
        }

        public void Delete(T entity)
        {
            _context.Set<T>()
                .Remove(entity);
        }

        public async Task<IEnumerable<T>> GetAllByPredicate(
            Expression<Func<T, bool>> predicate,
            CancellationToken cancellationToken)
        {
            return await _context.Set<T>()
                .AsNoTracking()
                .Where(predicate)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<T>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Set<T>()
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task<T?> GetByPredicate(
           Expression<Func<T, bool>> predicate,
           CancellationToken cancellationToken)
        {
            return await _context.Set<T>()
                .AsNoTracking()
                .FirstOrDefaultAsync(predicate, cancellationToken);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }
    }
}
