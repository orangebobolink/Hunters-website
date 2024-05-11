namespace Modules.Document.Application.Dtos.ResponseDtos
{
    public record class TripParticipantResponseDto
    {
        public Guid Id { get; init; }
        public Guid ParticipantId { get; init; }
        public UserResponseDto? Participant { get; init; }
        public Guid HuntingLicenseId { get; init; }
        public HuntingLicenseResponseDto? HuntingLicense { get; init; }
        public Guid TripId { get; init; }
    }
}
