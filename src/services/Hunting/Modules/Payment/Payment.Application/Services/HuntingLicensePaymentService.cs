using MassTransit;
using Payment.Application.Interfaces;
using Shared.Messages.HunterLicenseMessage;

namespace Payment.Application.Services
{
    internal class HuntingLicensePaymentService(
        IBus bus)
        : IHuntingLicensePaymentService
    {
        private readonly IBus _bus = bus;

        public async Task<bool> TryToPayAsync(
            string huntingLicense,
            CancellationToken cancellationToken)
        {
            var message = new PaymentHuntingLicense
            {
                IsPaid = true
            };

            await _bus.Publish(message);

            return true;
        }
    }
}
