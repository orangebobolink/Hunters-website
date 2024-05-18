using Chat.Services.Dtos.RequestDtos;
using Chat.Services.Dtos.ResponseDtos;

namespace Chat.Services.Interfaces
{
    public interface IMessageService
    {
        public Task<bool> CreateAsync(MessageRequestDto message);
    }
}
