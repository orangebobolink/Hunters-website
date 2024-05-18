using Chat.Interfaces.Repositories;
using Chat.Services.Dtos.ResponseDtos;
using Chat.Services.Interfaces;
using Mapster;
using Microsoft.Extensions.Logging;

namespace Chat.Services.Services
{
    internal class GroupService(
        IGroupRepository groupRepository,
        ILogger<GroupService> logger)
        : IGroupService
    {
        private readonly IGroupRepository _groupRepository = groupRepository;
        private readonly ILogger<GroupService> _logger = logger;

        public async Task<List<ResponseUserDto>> GetAllGroupsByUserId(Guid id)
        {
            var chats = await _groupRepository.GetGroupsByUserIdAsync(id);

            var users = chats
                .Select(c =>
                {
                    var user = c.Users
                            .Where(u => u.UserId != id)
                            .ToList()[0].User;
                    user!.Messages = c.Messages
                                .OrderBy(m=>m.CreateTime)
                                .ToList();

                    var response = user.Adapt<ResponseUserDto>();
                    response.GroupId = c.Id;

                    return response;
                })
                .ToList();

            return users;
        }
    }
}
