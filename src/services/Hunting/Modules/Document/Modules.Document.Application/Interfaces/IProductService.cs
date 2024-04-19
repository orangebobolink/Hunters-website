namespace Modules.Document.Application.Interfaces
{
    public interface IProductService
    {
        Task<List<string>> GetAllAsync(CancellationToken cancellationToken);
    }
}
