namespace Modules.Document.Application.Dtos.ResponseDtos
{
    public record class TripResponseDto
    {
        public Guid Id { get; init; }
        public string Number { get; init; } = string.Empty;
        public Guid PermissionId { get; init; }
        public PermissionResponseDto? Permission { get; init; }
        public string SpecialConditions { get; init; } = string.Empty;
        public DateTime EventDate { get; init; }
        public ICollection<TripParticipantResponseDto> TripParticipants { get; init; } = [];
        public decimal Price { get; init; }
        public DateTime ReturnedDate { get; init; }
        public string Status { get; init; } = null!;
    }
}
