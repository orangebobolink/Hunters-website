using Chat.Domain.Entities;

namespace Chat.Interfaces.Repositories
{
    public interface IGroupRepository
         : IRepository<Group>, ISoftDelete<Group>
    {
        List<Group> GetGroupsByUserId(int userId);
    }
}
