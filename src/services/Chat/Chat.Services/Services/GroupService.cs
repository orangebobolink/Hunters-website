using Chat.Interfaces.Repositories;
using Chat.Services.Dtos.ResponseDtos;
using Chat.Services.Interfaces;
using Mapster;
using Microsoft.Extensions.Logging;

namespace Chat.Services.Services
{
    internal class GroupService(IGroupRepository groupRepository, ILogger<GroupService> logger)
        : IGroupService
    {
        private readonly IGroupRepository _groupRepository = groupRepository;
        private readonly ILogger<GroupService> _logger = logger;

        public async Task<List<ResponseGroupDto>> GetAllGroupsByUserId(Guid id)
        {
            var chats = await _groupRepository.GetGroupsByUserIdAsync(id);

            var response = chats.Adapt<List<ResponseGroupDto>>();

            return response;
        }
    }
}
