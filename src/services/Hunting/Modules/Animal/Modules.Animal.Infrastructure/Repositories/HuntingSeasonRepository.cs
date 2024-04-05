using Microsoft.EntityFrameworkCore;
using Modules.Animal.Domain.Entities;
using Modules.Animal.Domain.Interfaces.Repositories;
using Modules.Animal.Infrastructure.Contexts;

namespace Modules.Animal.Infrastructure.Repositories
{
    internal class HuntingSeasonRepositpry(ApplicationDbContext context)
        : BaseRepository<HuntingSeason>(context), IHuntingSeasonRepository
    {
        public override async Task<List<HuntingSeason>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.HuntingSeasons
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public override async Task<HuntingSeason?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.HuntingSeasons
                .AsNoTracking()
                .FirstOrDefaultAsync(g => g.Id == id, cancellationToken);
        }

        public async Task<List<HuntingSeason>> GetHuntingSeasonsByTimePeriod(DateTime startDate, 
                                                                                DateTime endDate, 
                                                                                CancellationToken cancellationToken)
        {
            return await _context.HuntingSeasons
                .AsNoTracking()
                .Where(hs => hs.StartDate >= startDate 
                            && hs.EndDate <= endDate)
                .ToListAsync(cancellationToken);
        }
    }
}
