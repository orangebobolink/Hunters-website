using Chat.Domain.Entities;
using Chat.Interfaces.Repositories;
using Mapster;
using MassTransit;
using Microsoft.Extensions.Logging;
using Shared.Messages.UserMessages;

namespace Chat.Services.MassTransit.Consumers
{
    public class CreateUserConsumer(IUserRepository userRepository, ILogger<CreateUserConsumer> logger)
                : IConsumer<CreateUserMessage>
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly ILogger<CreateUserConsumer> _logger = logger;

        public async Task Consume(ConsumeContext<CreateUserMessage> context)
        {
            var user = context.Message
                .Adapt<User>();

            _userRepository.Create(user);

            await _userRepository.SaveChangesAsync();

            _logger.LogInformation("User was created");
        }
    }
}
