using Modules.Document.Application.Interfaces;
using Modules.Document.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using Mapster;
using Shared.Helpers;
using Modules.Document.Application.Dtos.ResponseDtos;

namespace Modules.Document.Application.Services
{
    internal class CouponService(
        ICouponRepository couponRepository,
        ILogger<CouponService> logger) : ICouponService
    {
        private readonly ICouponRepository _couponRepository = couponRepository;
        private readonly ILogger<CouponService> _logger = logger;

        public async Task<List<CouponResponseDto>> GetAllAsync(
            CancellationToken cancellationToken)
        {
            var coupons = await _couponRepository.GetAllAsync(cancellationToken);

            var response = coupons.Adapt<List<CouponResponseDto>>();

            return response;
        }

        public async Task<List<CouponResponseDto>> GetAllOnlyIsNotUsedAsync(
            CancellationToken cancellationToken)
        {
            var coupons = await _couponRepository.GetAllAsync(cancellationToken);

            var response = coupons
                .Where(c => c.IsUsed == false)
                .Adapt<List<CouponResponseDto>>();

            return response;
        }

        public async Task<CouponResponseDto> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken)
        {
            var coupon = await _couponRepository.GetByPredicate(
                e => e.Id == id,
                cancellationToken);

            if (coupon is null)
            {
                _logger.LogWarning("Id is null");
                ThrowHelper.ThrowKeyNotFoundException(nameof(id));
            }

            var response = coupon.Adapt<CouponResponseDto>();

            return response;
        }

        public async Task<CouponResponseDto> UpdateIsUsedAsync(
            Guid id,
            CancellationToken cancellationToken)
        {
            var coupon = await _couponRepository.GetByPredicate(
                e => e.Id == id,
                cancellationToken);

            if (coupon is null)
            {
                _logger.LogWarning("Id is null");
                ThrowHelper.ThrowKeyNotFoundException(nameof(id));
            }

            coupon!.IsUsed = true;
            _couponRepository.Update(coupon);

            await _couponRepository.SaveChangesAsync(cancellationToken);

            var response = coupon.Adapt<CouponResponseDto>();

            return response;
        }
    }
}
