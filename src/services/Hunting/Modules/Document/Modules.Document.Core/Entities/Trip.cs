namespace Modules.Document.Domain.Entities
{
    public class Trip
    {
        public Guid Id { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string Number { get; set; } = string.Empty;
        public Guid PermissionId { get; set; }
        public PermissionForExtractionOfHuntingAnimal? Permission { get; set; }
        public string SpecialConditions { get; set; } = string.Empty;
        public Guid IssuedId { get; set; }
        public User? Issued { get; set; }
        public Guid ReceivedId { get; set; }
        public User? Received { get; set; }
        public DateTime ReceivedDate = DateTime.Now;
        public List<TripParticipant> TripParticipants { get; set; } = [];
        public DateTime ReturnedDate { get; set; }
        public bool IsReturned { get; set; } = false;
        public Guid AcceptedId { get; set; }
        public User? Accepted { get; set; }
        public decimal AmountOfFee { get; set; }
    }
}
