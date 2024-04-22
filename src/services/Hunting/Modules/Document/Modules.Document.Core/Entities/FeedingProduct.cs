using Modules.Document.Domain.Enums;
using Modules.Document.Domain.Interfaces;

namespace Modules.Document.Domain.Entities
{
    public class FeedingProduct : IBaseEntity
    {
        public Guid Id { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public string UnitOfMeasurement { get; set; } = string.Empty;
        public ICollection<Feeding> Feedings { get; set; } = [];
    }
}
