using Mapster;
using MassTransit;
using Microsoft.Extensions.Logging;
using Modules.Document.Domain.Entities;
using Modules.Document.Domain.Interfaces;
using Shared.Messages.DataSeedMessages;

namespace Hunting.Bus.Consumers
{
    public class UserDataSeedConsumer(IUserRepository userRepository, ILogger<UserDataSeedConsumer> logger)
                : IConsumer<UserDataSeedMessage>
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly ILogger<UserDataSeedConsumer> _logger = logger;

        public async Task Consume(ConsumeContext<UserDataSeedMessage> context)
        {
            if (!(await _userRepository.GetAllAsync(CancellationToken.None)).Any())
            {
                var users = context.Message.Users
                    .Adapt<List<User>>();

                users.ForEach(_userRepository.Create);

                await _userRepository.SaveChangesAsync(CancellationToken.None);

                _logger.LogInformation("Users was created");
            }
        }
    }
}
