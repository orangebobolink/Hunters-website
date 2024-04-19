using Microsoft.AspNetCore.Mvc;
using Modules.Document.Application.Interfaces;

namespace Modules.Document.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponController : ControllerBase
    {
        private readonly ICouponService _couponService;

        public CouponController(ICouponService couponService)
        {
            _couponService = couponService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
        {
            var coupons = await _couponService.GetAllAsync(cancellationToken);
            return Ok(coupons);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken = default)
        {
            var coupon = await _couponService.GetByIdAsync(id, cancellationToken);

            return Ok(coupon);
        }

        [HttpGet("not-used")]
        public async Task<IActionResult> GetAllOnlyIsNotUsed(CancellationToken cancellationToken = default)
        {
            var coupons = await _couponService.GetAllOnlyIsNotUsedAsync(cancellationToken);
            return Ok(coupons);
        }

        [HttpPut("{id}/mark-used")]
        public async Task<IActionResult> MarkCouponAsUsed(Guid id, CancellationToken cancellationToken = default)
        {
            var updatedCoupon = await _couponService.UpdateIsUsedAsync(id, cancellationToken);
            return Ok(updatedCoupon);
        }
    }
}
