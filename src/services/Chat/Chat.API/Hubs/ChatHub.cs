using Chat.Services.Interfaces;
using Microsoft.AspNetCore.SignalR;
using SignalRSwaggerGen.Attributes;

namespace Chat.API.Hubs
{
    [SignalRHub]
    public class ChatHub(IGroupService groupService) : Hub
    {
        private readonly IGroupService _groupService = groupService;

        public async Task SendMessageToCaller(Guid userId, 
            [SignalRHidden] CancellationToken cancellationToken = default)
        {
            var dataToSend = await _groupService.GetAllGroupsByUserId(userId);

            await Clients.Caller.SendAsync("ReceiveMessage", dataToSend, cancellationToken);
        }
    }
}
