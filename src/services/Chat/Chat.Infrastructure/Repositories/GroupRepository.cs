using Chat.Domain.Entities;
using Chat.Interfaces.Repositories;

namespace Chat.Infrastructure.Repositories
{
    internal class GroupRepository
        : IGroupRepository
    {
        public void Create(Group entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<Group> GetAll()
        {
            throw new NotImplementedException();
        }

        public Group GetById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Group> GetGroupsByUserId(int userId)
        {
            throw new NotImplementedException();
        }

        public Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public void SoftDelete(Group entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Group entity)
        {
            throw new NotImplementedException();
        }
    }
}
