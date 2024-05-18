namespace Shared.Redis
{
    public interface ICacheService
    {
        Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken)
            where T : class;
        Task<T?> GetAsync<T>(string key, Func<Task<T>> factory, CancellationToken cancellationToken)
            where T : class;
        Task SetDataAsync<T>(string key, T value, CancellationToken cancellationToken)
            where T : class;
        Task RemoveDataAsync(string key, CancellationToken cancellationToken);
        Task RefreshDataAsync<T>(string key, T value, CancellationToken cancellationToken)
            where T : class;
    }
}
