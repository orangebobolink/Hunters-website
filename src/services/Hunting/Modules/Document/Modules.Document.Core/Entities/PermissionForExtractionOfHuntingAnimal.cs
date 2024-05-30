using Modules.Document.Domain.Enums;
using Modules.Document.Domain.Interfaces;

namespace Modules.Document.Domain.Entities
{
    public class PermissionForExtractionOfHuntingAnimal : IBaseEntity
    {
        public Guid Id { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string Number { get; set; } = string.Empty;
        public Guid AnimalId { get; set; }
        public Animal? Animal { get; set; }
        public Guid IssuedId { get; set; }
        public User? Issued { get; set; }
        public Guid ReceivedId { get; set; }
        public User? Received { get; set; }
        public ICollection<Coupon> Coupons { get; set; } = [];
        public Guid LandId { get; set; }
        public Land? Land { get; set; }
        public Status Status { get; set; } = Status.Compiled;
    }
}
