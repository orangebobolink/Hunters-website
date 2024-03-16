namespace Chat.Interfaces.Repositories
{
    public interface IRepository<T>
        where T : class
    {
        public T GetById(int id);
        public List<T> GetAll();
        void Create(T entity);
        void Update(T entity);
        void Delete(Guid id);
        Task SaveChangesAsync();
    }
}
