using Chat.Domain.Entities;

namespace Chat.Interfaces.Repositories
{
    public interface IMessageRepository
        : IRepository<Message>, ISoftDelete<Message>
    {
        List<Message> GetMessagesByUserId(int userId);
    }
}
