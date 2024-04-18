using Modules.Document.Domain.Enums;

namespace Modules.Document.Domain.Entities
{
    public class FeedingProduct
    {
        public Guid Id { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public string UnitOfMeasurement { get; set; } = string.Empty;
        public List<Feeding> Feedings { get; set; } = [];
    }
}
