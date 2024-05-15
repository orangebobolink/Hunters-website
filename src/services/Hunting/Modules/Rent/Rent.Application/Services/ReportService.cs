using Mapster;
using Microsoft.Extensions.Logging;
using Rent.Application.Dtos.ResponseDtos;
using Rent.Application.Interfaces;
using Rent.Domain.Enums;
using Rent.Domain.Interfaces;

namespace Rent.Application.Services
{
    internal class ReportService(
        IProductRepository productRepository,
        IRentProductRepository rentProductRepository,
        ILogger<ReportService> logger)
        : IReportService
    {
        private readonly IProductRepository _productRepository = productRepository;
        private readonly IRentProductRepository _rentProductRepository = rentProductRepository;
        private readonly ILogger<ReportService> _logger = logger;

        public async Task<List<ProductPopularResponseDto>> GetProductsByPopularAsync(
            Period period,
            CancellationToken cancellationToken)
        {
            var rentedProducts = await _rentProductRepository
                .GetAllByPredicate(
                rp => (rp.Status == RentStatus.Returned || rp.Status == RentStatus.Rented)
                        && rp.FromDate >= GetPeriodData(period),
                cancellationToken,
                true);

            var groupedProductInfo = rentedProducts
                .GroupBy(rp => rp.ProductId)
                .Select(group => new
                {
                    ProductId = group.Key,
                    group.First().Product.Name,
                    group.First().Product.Price,
                    RentedQuantity = group.Sum(rp => 1)
                })
                .Adapt<List<ProductPopularResponseDto>>();

            var popularProductsSorted = groupedProductInfo
                .OrderByDescending(dto => dto.RentedQuantity)
                .ToList();

            return popularProductsSorted;
        }

        public async Task<List<ProductPeriodRevenueResponseDto>> GetProductsRevenueByPeriodAsync(
            Period period,
            CancellationToken cancellationToken)
        {
            var rentedProducts = await _rentProductRepository
                .GetAllByPredicate(
                rp => (rp.Status == RentStatus.Returned || rp.Status == RentStatus.Rented)
                        && rp.FromDate >= GetPeriodData(period),
                cancellationToken,
                true);

            var groupedProductInfo = rentedProducts
                .GroupBy(rp => rp.ProductId)
                .Select(group => new
                {
                    ProductId = group.Key,
                    group.First()
                        .Product.Name,
                    Revenue = group.Sum(
                        rp => rp.Product.Price)
                })
                .Adapt<List<ProductPeriodRevenueResponseDto>>();

            return groupedProductInfo;
        }

        private DateTime GetPeriodData(Period period)
        {
            return period switch
            {
                Period.Week => DateTime.UtcNow.Subtract(TimeSpan.FromDays(7)),
                Period.Month => DateTime.UtcNow.Subtract(TimeSpan.FromDays(30)),
                Period.Year => DateTime.UtcNow.Subtract(TimeSpan.FromDays(365)),
                Period.Ever => DateTime.MinValue,
                _ => DateTime.UtcNow,
            };
        }
    }
}
