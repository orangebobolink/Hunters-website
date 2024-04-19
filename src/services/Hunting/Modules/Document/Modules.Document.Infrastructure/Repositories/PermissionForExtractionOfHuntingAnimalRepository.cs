using Microsoft.EntityFrameworkCore;
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
        public Task<List<PermissionForExtractionOfHuntingAnimal>> GetAllIncludeAsync(CancellationToken cancellationToken)
        {
            return _context.PermissionForExtractionOfHuntingAnimals
                .Include(p => p.Animal)
                .Include(p => p.Issued)
                .Include(p => p.Received)
                .ToListAsync(cancellationToken);
        }

        public Task<PermissionForExtractionOfHuntingAnimal?> GetByIdIncludeAsync(Guid id, CancellationToken cancellationToken)
        {
            return _context.PermissionForExtractionOfHuntingAnimals
                .Include(p => p.Animal)
                .Include(p => p.Issued)
                .Include(p => p.Received)
                .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
        }
    }
}
