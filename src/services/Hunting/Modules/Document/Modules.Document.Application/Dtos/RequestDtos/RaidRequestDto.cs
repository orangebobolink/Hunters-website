namespace Modules.Document.Application.Dtos.RequestDtos
{
    public class RaidRequestDto
    {
        public DateTime ExitTime { get; set; }
        public DateTime ReturnedTime { get; set; }
        public List<UserDto> Participants { get; set; } = [];
        public string Note { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public Guid LandId { get; set; }
    }
}
