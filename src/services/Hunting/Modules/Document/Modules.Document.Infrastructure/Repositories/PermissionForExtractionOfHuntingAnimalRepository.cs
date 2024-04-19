using Modules.Document.Domain.Entities;
using Modules.Document.Domain.Interfaces;
using Modules.Document.Infrastructure.Contexts;

namespace Modules.Document.Infrastructure.Repositories
{
    internal class PermissionForExtractionOfHuntingAnimalRepository(
        DocumentDbContext context)
        : BaseRepository<PermissionForExtractionOfHuntingAnimal>(context),
        IPermissionForExtractionOfHuntingAnimalRepository

    {
    }
}
