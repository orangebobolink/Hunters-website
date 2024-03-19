namespace Chat.Interfaces.Repositories
{
    public interface IRepository<T>
        where T : class
    {
        public Task<T?> GetByIdAsync(Guid id);
        public Task<List<T>> GetAllAsync();
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task SaveChangesAsync();
    }
}
