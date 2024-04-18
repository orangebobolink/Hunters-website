using Modules.Animal.Domain.Entities;

namespace Modules.Animal.Domain.Interfaces.Repositories
{
    public interface IAnimalRepository 
        : IRepository<AnimalInfo>
    {
        public Task<List<AnimalInfo>> GetAllWithFullInformationAsync(
            CancellationToken cancellationToken);
        public Task<AnimalInfo?> GetByNameAsync(
            string name, 
            CancellationToken cancellationToken);
    }
}
