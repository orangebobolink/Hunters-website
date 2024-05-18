using Chat.Domain.Entities;

namespace Chat.Interfaces.Repositories
{
    public interface IGroupRepository
         : IRepository<Group>, ISoftDelete<Group>
    {
        Task<List<Group>> GetGroupsByUserIdAsync(Guid userId);
    }
}
