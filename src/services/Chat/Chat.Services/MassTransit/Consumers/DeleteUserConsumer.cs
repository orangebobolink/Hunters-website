using Chat.Domain.Entities;
using Chat.Interfaces.Repositories;
using Mapster;
using MassTransit;
using Microsoft.Extensions.Logging;
using Shared.Messages.UserMessages;

namespace Chat.Services.MassTransit.Consumers
{
    public class DeleteUserConsumer(IUserRepository userRepository, ILogger<DeleteUserConsumer> logger)
                : IConsumer<DeleteUserMessage>
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly ILogger<DeleteUserConsumer> _logger = logger;

        public async Task Consume(ConsumeContext<DeleteUserMessage> context)
        {
            var user = context.Message
                .Adapt<User>();

            _userRepository.Delete(user);

            await _userRepository.SaveChangesAsync();

            _logger.LogInformation("User was deleted");
        }
    }
}
