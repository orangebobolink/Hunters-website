using Modules.Document.Domain.Entities;

namespace Modules.Document.Application.Dtos.ResponseDtos
{
    public class RaidResponseDto
    {
        public Guid Id { get; set; }
        public DateTime ExitTime { get; set; }
        public DateTime ReturnedTime { get; set; }
        public List<UserResponseDto> Participants { get; set; } = [];
        public string Note { get; set; } = string.Empty;
    }
}
