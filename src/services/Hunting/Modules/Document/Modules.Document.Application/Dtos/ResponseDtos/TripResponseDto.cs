namespace Modules.Document.Application.Dtos.ResponseDtos
{
    public class TripResponseDto
    {
        public Guid Id { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string Number { get; set; } = string.Empty;
        public Guid PermissionId { get; set; }
        public PermisionResponseDto? Permission { get; set; }
        public string SpecialConditions { get; set; } = string.Empty;
        public Guid IssuedId { get; set; }
        public UserResponseDto? Issued { get; set; }
        public Guid ReceivedId { get; set; }
        public UserResponseDto? Received { get; set; }
        public DateTime ReceivedDate { get; set; }
        public DateTime EventDate { get; set; }
        public List<TripParticipantResponseDto> TripParticipants { get; set; } = [];
        public DateTime ReturnedDate { get; set; }
        public bool IsReturned { get; set; } = false;
        public Guid AcceptedId { get; set; }
        public UserResponseDto? Accepted { get; set; }
        public decimal AmountOfFee { get; set; }
    }
}
