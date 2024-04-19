using Mapster;
using MassTransit;
using Microsoft.Extensions.Logging;
using Modules.Document.Domain.Entities;
using Modules.Document.Domain.Interfaces;
using Shared.Messages.HunterLicenseMessage;

namespace Hunting.Bus.Consumers
{
    public class HuntingLicenseConsumer(IHuntingLicenseRepository huntingLicenseRepository, ILogger<HuntingLicenseConsumer> logger) : IConsumer<CreateHuntingLicense>
    {
        private readonly IHuntingLicenseRepository _huntingLicenseRepository = huntingLicenseRepository;
        private readonly ILogger<HuntingLicenseConsumer> _logger = logger;

        public async Task Consume(ConsumeContext<CreateHuntingLicense> context)
        {
            var huntingLicense = context.Message
                .Adapt<HuntingLicense>();

            _huntingLicenseRepository.Create(huntingLicense);

            await _huntingLicenseRepository.SaveChangesAsync(CancellationToken.None);

            _logger.LogInformation("Hunting license was created");
        }
    }
}
