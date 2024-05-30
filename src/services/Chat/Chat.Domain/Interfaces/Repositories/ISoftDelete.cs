namespace Chat.Interfaces.Repositories
{
    public interface ISoftDelete<T>
        where T : class
    {
        void SoftDelete(T entity);
    }
}
