using Chat.Services.Interfaces;
using Microsoft.AspNetCore.SignalR;
using SignalRSwaggerGen.Attributes;

namespace Chat.API.Hubs
{
    [SignalRHub]
    public class ChatHub(IGroupService groupService) : Hub
    {
        private readonly IGroupService _groupService = groupService;

        public async Task ReceiveMessages(string id)
        {
            var dataToSend = await _groupService.GetAllGroupsByUserId(Guid.Parse(id));

            // return dataToSend;
            await Clients.Caller.SendAsync("ReceiveMessages", dataToSend);
        }
    }
}
