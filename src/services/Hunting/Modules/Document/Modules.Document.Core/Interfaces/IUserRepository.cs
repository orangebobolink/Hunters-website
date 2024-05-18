using Modules.Document.Domain.Entities;

namespace Modules.Document.Domain.Interfaces
{
    public interface IUserRepository
        : IRepository<User>
    {
        Task<List<User>> GetAllExistsUsers(List<User> users);
    }
}
