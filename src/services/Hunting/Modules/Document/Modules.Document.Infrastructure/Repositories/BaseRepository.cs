using Microsoft.EntityFrameworkCore;
using Modules.Document.Domain.Interfaces;
using Modules.Document.Infrastructure.Contexts;

namespace Modules.Document.Infrastructure.Repositories
{
    internal class BaseRepository<T>(DocumentDbContext context) : IRepository<T>
        where T : class, IBaseEntity
    {
        protected readonly DocumentDbContext _context = context;

        public void Create(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public async Task<List<T>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Set<T>()
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Set<T>()
               .AsNoTracking()
               .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
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
