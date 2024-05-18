using Modules.Document.Domain.Interfaces;

namespace Modules.Document.Domain.Entities
{
    public class TripParticipant : IBaseEntity
    {
        public Guid Id { get; set; }
        public Guid ParticipantId { get; set; }
        public User? Participant { get; set; }
        public Guid HuntingLicenseId { get; set; }
        public HuntingLicense? HuntingLicense { get; set; }
        public Guid TripId { get; set; }
        public Trip? Trip { get; set; }
        public bool Paid { get; set; }
    }
}
