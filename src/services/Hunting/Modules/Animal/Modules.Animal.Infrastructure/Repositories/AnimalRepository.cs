using Microsoft.EntityFrameworkCore;
using Modules.Animal.Domain.Entities;
using Modules.Animal.Domain.Interfaces.Repositories;
using Modules.Animal.Infrastructure.Contexts;
using System.Threading;

namespace Modules.Animal.Infrastructure.Repositories
{
    internal class AnimalRepository(ApplicationDbContext context)
        : BaseRepository<AnimalInfo>(context), IAnimalRepository
    {
        public override async Task<List<AnimalInfo>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Animals
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task<List<AnimalInfo>> GetAllWithFullInformationAsync(CancellationToken cancellationToken)
        {
            return await _context.Animals
                .AsNoTracking()
                .Include(a => a.HuntingSeasons)
                .ToListAsync(cancellationToken);
        }

        public override async Task<AnimalInfo?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Animals
                .AsNoTracking()
                .FirstOrDefaultAsync(g => g.Id == id, cancellationToken);
        }

        public async Task<AnimalInfo?> GetByNameAsync(string name, CancellationToken cancellationToken)
        {
            return await _context.Animals
                .AsNoTracking()
                .FirstOrDefaultAsync(g => g.Name == name, cancellationToken);
        }
    }
}
