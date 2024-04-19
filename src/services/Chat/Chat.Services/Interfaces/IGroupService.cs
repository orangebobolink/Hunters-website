using Chat.Services.Dtos.ResponseDtos;

namespace Chat.Services.Interfaces
{
    public interface IGroupService
    {
        public Task<List<ResponseGroupDto>> GetAllGroupsByUserId(Guid id);
    }
}
