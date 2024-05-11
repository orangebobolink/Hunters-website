using Mapster;
using Microsoft.EntityFrameworkCore;
using Modules.Document.Infrastructure.Contexts;
using Rent.Domain.Interfaces;
using System.Linq.Expressions;

namespace Rent.Infrastructure.Repositories
{
    internal class BaseRepository<T>(RentDbContext context)
        : IRepository<T>
        where T : class, IBaseEntity
    {
        protected readonly RentDbContext _context = context;

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

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }

        public async Task<T?> GetByPredicate(
            Expression<Func<T, bool>> predicate,
            CancellationToken cancellationToken = default,
            bool exceptIncludes = false,
            bool trackChanges = true)
        {
            var query = _context.Set<T>()
                        .Where(predicate)
                        .AsSplitQuery()
                        .AsNoTracking();

            var projectedQuery =
              exceptIncludes
              ? query.ProjectToType<T>()
              : query as IQueryable<T>;

            return await projectedQuery!
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<IEnumerable<T>> GetAllByPredicate(
            Expression<Func<T, bool>> predicate,
            CancellationToken cancellationToken = default,
            bool exceptIncludes = false,
            bool trackChanges = true)
        {
            var query = _context.Set<T>()
                        .Where(predicate)
                        .AsSplitQuery()
                        .AsNoTracking();

            var projectedQuery =
              exceptIncludes
              ? query.ProjectToType<T>()
              : query as IQueryable<T>;

            return await projectedQuery!
              .ToListAsync(cancellationToken);
        }

        public async Task<List<T>> GetAllAsync(
            CancellationToken cancellationToken = default,
            bool exceptIncludes = false,
            bool trackChanges = true)
        {
            var query = _context.Set<T>()
                        .AsSplitQuery()
                        .AsNoTracking();

            var projectedQuery =
                exceptIncludes
                ? query.ProjectToType<T>()
                : query as IQueryable<T>;

            return await projectedQuery!
               .ToListAsync(cancellationToken);
        }
    }
}
