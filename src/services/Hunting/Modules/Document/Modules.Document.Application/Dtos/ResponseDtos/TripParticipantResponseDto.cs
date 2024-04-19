namespace Modules.Document.Application.Dtos.ResponseDto
{
    public class TripParticipantResponseDto
    {
        public Guid Id { get; set; }
        public Guid ParticipantId { get; set; }
        public UserResponseDto? Participant { get; set; }
        public Guid HuntingLicenseId { get; set; }
        public HuntingLicenseResponseDto? HuntingLicense { get; set; }
        public Guid TripId { get; set; }
        public TripResponseDto? Trip { get; set; }
    }
}
