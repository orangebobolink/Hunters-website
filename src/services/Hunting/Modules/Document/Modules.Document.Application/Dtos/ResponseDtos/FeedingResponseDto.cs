namespace Modules.Document.Application.Dtos.ResponseDtos
{
    public record class FeedingResponseDto
    {
        public Guid Id { get; init; }
        public string Number { get; init; } = string.Empty;
        public DateTime FeedingDate { get; init; }
        public Guid IssuedId { get; init; }
        public UserResponseDto? Issued { get; init; }
        public Guid ReceivedId { get; init; }
        public UserResponseDto? Received { get; init; }
        public DateTime ReceivedDate = DateTime.Now;
        public List<FeedingProductResponseDto> Products { get; init; } = [];
        public Guid LandId { get; init; }
        public LandResponseDto? Land { get; init; }
        public string Status { get; set; } = string.Empty;
    }
}
