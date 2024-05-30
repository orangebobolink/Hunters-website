using MassTransit;
using Payment.Application.Interfaces;

namespace Payment.Application.Services
{
    internal class TripPaymentService(
        IBus bus)
        : ITripPaymentService
    {
        private readonly IBus _bus = bus;

        public async Task<bool> TryToPayAsync(
            string tripNumber,
            CancellationToken cancellationToken)
        {
            return true;
        }
    }
}
