using Rent.Domain.Entities;
using Rent.Domain.Enums;

namespace Rent.Application.Dtos.RequestDtos
{
    public class ProductRequestDto
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int QuantityInStock { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public ProductType Type { get; set; }
        public ICollection<RentProductRequestDto> RentProducts { get; set; } = [];
    }
}
