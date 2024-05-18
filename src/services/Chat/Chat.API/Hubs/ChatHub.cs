using Chat.Services.Dtos.RequestDtos;
using Chat.Services.Dtos.ResponseDtos;
using Chat.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SignalRSwaggerGen.Attributes;

namespace Chat.API.Hubs
{
    [SignalRHub]
    public class ChatHub(
        IGroupService groupService,
        IMessageService messageService)
        : Hub
    {
        private readonly IGroupService _groupService = groupService;
        private readonly IMessageService _messageService = messageService;

        public async Task<List<ResponseUserDto>> ReceiveMessages(Guid id)
        {
            var dataToSend = await _groupService.GetAllGroupsByUserId(id);

            return dataToSend;
        }

        public async Task CreateMessage(
            MessageRequestDto message)
        {
            await _messageService.CreateAsync(message);

            await Clients.All.SendAsync("NewMessage", message);
        }
    }
}
