using Rent.Application.Dtos.ResponseDtos;
using Rent.Domain.Enums;

namespace Rent.Application.Interfaces
{
    public interface IReportService
    {
        Task<List<ProductPeriodRevenueResponseDto>> GetProductsRevenueByPeriodAsync(Period period, CancellationToken cancellationToken);
        Task<List<ProductPopularResponseDto>> GetProductsByPopularAsync(Period period, CancellationToken cancellationToken);
    }
}
