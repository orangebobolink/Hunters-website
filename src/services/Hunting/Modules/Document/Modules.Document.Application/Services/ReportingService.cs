using Mapster;
using Modules.Document.Application.Dtos.RequestDtos;
using Modules.Document.Application.Interfaces;
using Modules.Document.Domain.Enums;
using Modules.Document.Domain.Interfaces;

namespace Modules.Document.Application.Services
{
    internal class ReportingService(
        IAnimalRepository animalRepository,
        ITripRepository tripRepository)
        : IReportingService
    {
        private readonly IAnimalRepository _animalRepository = animalRepository;
        private readonly ITripRepository _tripRepository = tripRepository;

        public async Task<IEnumerable<AnimalPopularRequestDto>> GetAnimalsByPopular(
            PeriodReport period,
            CancellationToken cancellationToken)
        {
            var canceledTrips = await _tripRepository
                .GetAllByPredicate(
                rp => rp.Status == Status.Completed
                        && rp.Permission.ToDate >= GetPeriodData(period),
                cancellationToken,
                true);

            var animals = await _animalRepository.GetAllAsync();

            var groupedAnimals = canceledTrips
              .GroupBy(rp => rp.Permission.AnimalId)
              .Select(group => new
              {
                  AnimalId = group.Key,
                  Name = animals.FirstOrDefault(a => a.Id == group.Key)?.Name!,
                  Quantity = group.Sum(p => p.TripParticipants.Count),
              })
              .Adapt<List<AnimalPopularRequestDto>>();

            return groupedAnimals;
        }

        private DateTime GetPeriodData(PeriodReport period)
        {
            return period switch
            {
                PeriodReport.Week => DateTime.UtcNow.Subtract(TimeSpan.FromDays(7)),
                PeriodReport.Month => DateTime.UtcNow.Subtract(TimeSpan.FromDays(30)),
                PeriodReport.Year => DateTime.UtcNow.Subtract(TimeSpan.FromDays(365)),
                PeriodReport.Ever => DateTime.MinValue,
                _ => DateTime.UtcNow,
            };
        }
    }
}
