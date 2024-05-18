using Modules.Document.Domain.Entities;

namespace Modules.Document.Domain.Interfaces
{
    public interface IPermissionForExtractionOfHuntingAnimalRepository
        : IRepository<PermissionForExtractionOfHuntingAnimal>,
        IGetInclude<PermissionForExtractionOfHuntingAnimal>
    {
    }
}
