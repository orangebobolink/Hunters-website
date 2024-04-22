namespace Modules.Document.Application.Dtos.ResponseDtos
{
    public record class TripResponseDto
    {
        public Guid Id { get; init; }
        public DateTime FromDate { get; init; }
        public DateTime ToDate { get; init; }
        public string Number { get; init; } = string.Empty;
        public Guid PermissionId { get; init; }
        public PermissionResponseDto? Permission { get; init; }
        public string SpecialConditions { get; init; } = string.Empty;
        public Guid IssuedId { get; init; }
        public UserResponseDto? Issued { get; init; }
        public Guid ReceivedId { get; init; }
        public UserResponseDto? Received { get; init; }
        public DateTime ReceivedDate { get; init; }
        public DateTime EventDate { get; init; }
        public List<TripParticipantResponseDto> TripParticipants { get; init; } = [];
        public DateTime ReturnedDate { get; init; }
        public bool IsReturned { get; init; } = false;
        public Guid AcceptedId { get; init; }
        public UserResponseDto? Accepted { get; init; }
        public decimal AmountOfFee { get; init; }
    }
}
