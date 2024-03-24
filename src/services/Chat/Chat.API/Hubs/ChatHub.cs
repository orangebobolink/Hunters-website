using Chat.Services.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace Chat.API.Hubs
{
    public class ChatHub(IGroupService groupService) : Hub
    {
        private readonly IGroupService _groupService = groupService;

        public async Task SendMessageToCaller(Guid userId)
        {
            var dataToSend = await _groupService.GetAllGroupsByUserId(userId);

            await Clients.Caller.SendAsync("ReceiveMessage", dataToSend);
        }
    }
}
