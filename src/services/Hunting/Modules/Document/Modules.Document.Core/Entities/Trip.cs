using Modules.Document.Domain.Enums;
using Modules.Document.Domain.Interfaces;

namespace Modules.Document.Domain.Entities
{
    public class Trip : IBaseEntity
    {
        public Guid Id { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string Number { get; set; } = string.Empty;
        public Guid PermissionId { get; set; }
        public PermissionForExtractionOfHuntingAnimal? Permission { get; set; }
        public string SpecialConditions { get; set; } = string.Empty;
        public DateTime EventDate { get; set; }
        public ICollection<TripParticipant> TripParticipants { get; set; } = [];
        public DateTime ReturnedDate { get; set; }
        public Guid AcceptedId { get; set; }
        public User? Accepted { get; set; }
        public decimal AmountOfFee { get; set; }
        public Status Status { get; set; } = Status.Compiled;
    }
}
