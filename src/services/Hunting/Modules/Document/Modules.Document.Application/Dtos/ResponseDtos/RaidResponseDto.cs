namespace Modules.Document.Application.Dtos.ResponseDtos
{
    public record class RaidResponseDto
    {
        public Guid Id { get; init; }
        public DateTime ExitTime { get; init; }
        public DateTime ReturnedTime { get; init; }
        public List<UserResponseDto> Participants { get; init; } = [];
        public string Note { get; init; } = string.Empty;
        public Guid LandId { get; init; }
        public LandResponseDto? Land { get; init; }
    }
}
