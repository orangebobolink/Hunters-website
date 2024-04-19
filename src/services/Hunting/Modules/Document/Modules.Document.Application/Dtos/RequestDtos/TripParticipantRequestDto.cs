namespace Modules.Document.Application.Dtos.RequestDtos
{
    public class TripParticipantRequestDto
    {
        public Guid ParticipantId { get; set; }
        public UserRequestDto? Participant { get; set; }
        public Guid HuntingLicenseId { get; set; }
        public HuntingLicenseRequestDto? HuntingLicense { get; set; }
        public Guid TripId { get; set; }
        public TripRequestDto? Trip { get; set; }
    }
}
