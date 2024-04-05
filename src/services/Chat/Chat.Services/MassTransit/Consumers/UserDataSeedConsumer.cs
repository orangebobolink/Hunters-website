using Chat.Domain.Entities;
using Chat.Interfaces.Repositories;
using Mapster;
using MassTransit;
using Microsoft.Extensions.Logging;
using Shared.Messages.DataSeedMessages;

namespace Chat.Services.MassTransit.Consumers
{
    public class UserDataSeedConsumer(IUserRepository userRepository, ILogger<UserDataSeedConsumer> logger)
                : IConsumer<UserDataSeedMessage>
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly ILogger<UserDataSeedConsumer> _logger = logger;

        public async Task Consume(ConsumeContext<UserDataSeedMessage> context)
        {
            // TODO: Add method to repos that will find ANY
            if(!(await _userRepository.GetAllAsync()).Any())
            {
                var users = context.Message.Users
                    .Adapt<List<User>>();

                users.ForEach(_userRepository.Create);

                await _userRepository.SaveChangesAsync();

                _logger.LogInformation("User was updated");
            }
        }
    }
}
