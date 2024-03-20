using Chat.Domain.Entities;
using Chat.Interfaces.Repositories;
using Mapster;
using MassTransit;
using Microsoft.Extensions.Logging;
using Shared.Messages.UserMessages;

namespace Chat.Services.MassTransit.Consumers
{
    internal class UpdateUserConsumer(IUserRepository userRepository, ILogger<UpdateUserConsumer> logger)
                : IConsumer<UpdateUserMessage>
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly ILogger<UpdateUserConsumer> _logger = logger;

        public async Task Consume(ConsumeContext<UpdateUserMessage> context)
        {
            var user = context.Message
                .Adapt<User>();

            _userRepository.Update(user);

            await _userRepository.SaveChangesAsync();

            _logger.LogInformation("User was updated");
        }
    }
}
