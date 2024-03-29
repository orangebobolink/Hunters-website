using Chat.Services.Dtos.ResponseDtos;
using Chat.Services.Interfaces;
using Microsoft.AspNetCore.SignalR;
using SignalRSwaggerGen.Attributes;

namespace Chat.API.Hubs
{
    [SignalRHub]
    public class ChatHub(IGroupService groupService) : Hub
    {
        private readonly IGroupService _groupService = groupService;

        public async Task<List<ResponseGroupDto>> ReceiveMessages(Guid id)
        {
            var dataToSend = await _groupService.GetAllGroupsByUserId(id);

            return dataToSend;
        }
    }
}
