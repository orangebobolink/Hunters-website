using Microsoft.EntityFrameworkCore;
using Modules.Document.Domain.Interfaces;
using Modules.Document.Infrastructure.Contexts;
using Modules.Document.Infrastructure.Extenstions;
using System.Linq.Expressions;

namespace Modules.Document.Infrastructure.Repositories
{
    internal class BaseRepository<T>(
        DocumentDbContext context)
        : IRepository<T>
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

        public async Task<T?> GetByPredicate(
           Expression<Func<T, bool>> predicate,
           CancellationToken cancellationToken = default,
           bool exceptIncludes = false,
           bool trackChanges = true)
        {
            return await _context.Set<T>()
                            .Where(predicate)
                            .AsSplitQuery()
                            .TrackChanges(trackChanges)
                            .ExceptIncludes(exceptIncludes)
                            .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<IEnumerable<T>> GetAllByPredicate(
            Expression<Func<T, bool>> predicate,
            CancellationToken cancellationToken = default,
            bool exceptIncludes = false,
            bool trackChanges = true)
        {
            return await _context.Set<T>()
                            .Where(predicate)
                            .AsSplitQuery()
                            .TrackChanges(exceptIncludes)
                            .ExceptIncludes(exceptIncludes)
                            .ToListAsync(cancellationToken);
        }

        public async Task<List<T>> GetAllAsync(
            CancellationToken cancellationToken = default,
            bool exceptIncludes = false,
            bool trackChanges = true)
        {
            return await _context.Set<T>()
                            .AsSplitQuery()
                            .TrackChanges(exceptIncludes)
                            .ExceptIncludes(exceptIncludes)
                            .ToListAsync(cancellationToken);
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
