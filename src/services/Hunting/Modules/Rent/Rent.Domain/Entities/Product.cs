using Rent.Domain.Enums;
using Rent.Domain.Interfaces;

namespace Rent.Domain.Entities
{
    public class Product : IBaseEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int QuantityInStock { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public ProductType Type { get; set; }
        public ICollection<RentProduct> RentProducts { get; set; } = [];
    }
}
