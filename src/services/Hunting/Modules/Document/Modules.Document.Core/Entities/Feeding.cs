using Modules.Document.Domain.Enums;
using Modules.Document.Domain.Interfaces;

namespace Modules.Document.Domain.Entities
{
    public class Feeding : IBaseEntity
    {
        public Guid Id { get; set; }
        public string Number { get; set; } = string.Empty;
        public DateTime FeedingDate { get; set; }
        public Guid IssuedId { get; set; }
        public User? Issued { get; set; }
        public Guid ReceivedId { get; set; }
        public User? Received { get; set; }
        public DateTime ReceivedDate { get; set; }
        public ICollection<FeedingProduct> Products { get; set; } = [];
        public Guid LandId { get; set; }
        public Land? Land { get; set; }
        public Status Status { get; set; } = Status.Compiled;
    }
}
