using Chat.Interfaces.Repositories;

namespace Chat.Infrastructure.Repositories
{
    internal abstract class BaseRepository<T>
        : IRepository<T>
        where T : class
    {
        public void Create(T entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Update(T entity)
        {
            throw new NotImplementedException();
        }

        public Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public abstract List<T> GetAll();

        public abstract T GetById(int id);
    }
}
