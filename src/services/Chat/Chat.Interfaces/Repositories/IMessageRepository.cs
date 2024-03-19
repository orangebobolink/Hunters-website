using Chat.Domain.Entities;

namespace Chat.Interfaces.Repositories
{
    public interface IMessageRepository
        : IRepository<Message>, ISoftDelete<Message>
    {
        Task<List<Message>> GetMessagesByUserId(Guid userId);
    }
}
