namespace Modules.Document.Application.Dtos.ResponseDto
{
    public class FeedingResponseDto
    {
        public Guid Id { get; set; }
        public string Number { get; set; } = string.Empty;
        public DateTime FeedingDate { get; set; }
        public Guid IssuedId { get; set; }
        public UserResponseDto? Issued { get; set; }
        public Guid ReceivedId { get; set; }
        public UserResponseDto? Received { get; set; }
        public DateTime ReceivedDate = DateTime.Now;
        public List<FeedingProductResponseDto> Products { get; set; } = [];
    }
}
