using Modules.Animal.Domain.Entities;

namespace Modules.Animal.Domain.Interfaces.Repositories
{
    public interface IHuntingSeasonRepository 
        : IRepository<HuntingSeason>
    {
        public Task<List<HuntingSeason>> GetHuntingSeasonsByTimePeriod(DateTime startDate, DateTime endDate, 
                                                                        CancellationToken cancellationToken);
    }
}
