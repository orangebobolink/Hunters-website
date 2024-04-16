namespace Shared.Redis
{
    public interface ICacheService
    {
        Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken)
            where T : class;
        Task SetData<T>(string key, T value, CancellationToken cancellationToken)
            where T : class;
        Task RemoveData(string key, CancellationToken cancellationToken);

        Task RefreshData<T>(string key, T value, CancellationToken cancellationToken)
            where T : class;
    }
}
