using Modules.Document.Domain.Enums;

namespace Modules.Document.Application.Dtos.RequestDtos
{
    public class FeedingRequestDto
    {
        public string Number { get; set; } = string.Empty;
        public DateTime FeedingDate { get; set; }
        public Guid IssuedId { get; set; }
        public UserRequestDto? Issued { get; set; }
        public Guid ReceivedId { get; set; }
        public UserRequestDto? Received { get; set; }
        public DateTime ReceivedDate = DateTime.Now;
        public List<FeedingProductRequestDto> Products { get; set; } = [];
        public string Status { get; set; } = string.Empty;
    }
}
