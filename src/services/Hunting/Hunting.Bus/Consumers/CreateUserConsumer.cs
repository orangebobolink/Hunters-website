using Mapster;
using MassTransit;
using Microsoft.Extensions.Logging;
using Shared.Messages.UserMessages;

using RentInterface = Rent.Domain.Interfaces;
using RentModel = Rent.Domain.Entities;
using DocumentInterface = Modules.Document.Domain.Interfaces;
using DocumentModel = Modules.Document.Domain.Entities;

namespace Hunting.Bus.Consumers
{
    public class CreateUserConsumer(
        RentInterface.IUserRepository userRepository,
        DocumentInterface.IUserRepository documentUserRepository,
        ILogger<CreateUserConsumer> logger)
                : IConsumer<CreateUserMessage>
    {
        private readonly RentInterface.IUserRepository _userRepository = userRepository;
        private readonly DocumentInterface.IUserRepository _documentUserRepository = documentUserRepository;
        private readonly ILogger<CreateUserConsumer> _logger = logger;

        public async Task Consume(ConsumeContext<CreateUserMessage> context)
        {
            var rentUser = context.Message
                .Adapt<RentModel.User>();
            var docUser = context.Message
                .Adapt<DocumentModel.User>();

            _userRepository.Create(rentUser);
            _documentUserRepository.Create(docUser);

            await _userRepository.SaveChangesAsync(CancellationToken.None);
            await _documentUserRepository.SaveChangesAsync(CancellationToken.None);

            _logger.LogInformation("User was created");
        }
    }
}
