using Modules.Document.Application.Dtos.RequestDtos;
using Modules.Document.Domain.Enums;

namespace Modules.Document.Application.Interfaces
{
    public interface IReportingService
    {
        Task<IEnumerable<AnimalPopularRequestDto>> GetAnimalsByPopular(
            PeriodReport period,
            CancellationToken cancellationToken);
    }
}
