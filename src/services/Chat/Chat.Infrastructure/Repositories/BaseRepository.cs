using Chat.Infrastructure.Contexts;
using Chat.Interfaces.Repositories;

namespace Chat.Infrastructure.Repositories
{
    internal abstract class BaseRepository<T>
        : IRepository<T>
        where T : class
    {
        protected readonly ApplicationDbContext _context;

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Create(T entity)
        {
            _context.Add(entity);
        }

        public void Delete(T entity)
        {
            _context.Remove(entity);
        }

        public void Update(T entity)
        {
            _context.Update(entity);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public abstract Task<List<T>> GetAllAsync();

        public abstract Task<T?> GetByIdAsync(Guid id);
    }
}
