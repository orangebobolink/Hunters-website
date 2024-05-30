using Identity.Domain.Interfaces;
using MassTransit;
using Microsoft.Extensions.Logging;
using Shared.Helpers;
using Shared.Messages.HunterLicenseMessage;

namespace Identity.Services.Consumers
{
    public class PaymentHuntingLicenseConsumer(
        IHyntingLicenseRepository huntingLicenseRepository,
        ILogger<PaymentHuntingLicenseConsumer> logger)
        : IConsumer<PaymentHuntingLicenseMessage>
    {
        private readonly IHyntingLicenseRepository _huntingLicenseRepository
            = huntingLicenseRepository;
        private readonly ILogger<PaymentHuntingLicenseConsumer> _logger = logger;

        public async Task Consume(ConsumeContext<PaymentHuntingLicenseMessage> context)
        {
            var huntingLicense = await _huntingLicenseRepository.GetByPredicate(
                hl => hl.LicenseNumber == context.Message.LicenseNumber,
                CancellationToken.None);

            if (huntingLicense is null)
            {
                ThrowHelper.ThrowKeyNotFoundException(context.Message.LicenseNumber);
            }

            huntingLicense!.IsPaid = context.Message.IsPaid;

            _huntingLicenseRepository.Update(huntingLicense);

            await _huntingLicenseRepository.SaveChangesAsync(CancellationToken.None);

            _logger.LogInformation("Hunting license was updated");
        }
    }
}
