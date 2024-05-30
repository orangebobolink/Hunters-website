using Chat.Domain.Entities;
using Chat.Interfaces.Repositories;
using Chat.Services.Dtos.RequestDtos;
using Chat.Services.Dtos.ResponseDtos;
using Chat.Services.Interfaces;
using Mapster;
using Microsoft.Extensions.Logging;

namespace Chat.Services.Services
{
    internal class MessageService(
        IMessageRepository messageRepository,
        IUserRepository userRepository,
        IGroupRepository groupRepository,
        ILogger<MessageService> logger)
        : IMessageService
    {
        private readonly IMessageRepository _messageRepository = messageRepository;
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IGroupRepository _groupRepository = groupRepository;
        private readonly ILogger<MessageService> _logger = logger;

        public async Task<bool> CreateAsync(MessageRequestDto requestMessage)
        {
            var existingUser = await _userRepository.GetByIdAsync(requestMessage.UserId);

            if (existingUser is null)
            {
                throw new Exception();
            }

            var existingGroup = await _groupRepository.GetByIdAsync(requestMessage.GroupId);

            if (existingGroup is null)
            {
                var group = new Group()
                {
                    Id = requestMessage.GroupId,
                    Users = [
                        new UserGroup() { UserId = existingUser.Id},
                        new UserGroup() { UserId = requestMessage.ToUserId }
                        ]
                };

                _groupRepository.Create(group);

                await _groupRepository.SaveChangesAsync();
            }

            var message = requestMessage.Adapt<Message>();
            message.Id = Guid.NewGuid();

            _messageRepository.Create(message);

            await _messageRepository.SaveChangesAsync();

            return true;
        }
    }
}
