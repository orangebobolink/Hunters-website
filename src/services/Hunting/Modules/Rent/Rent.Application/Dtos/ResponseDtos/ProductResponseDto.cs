using Rent.Application.Dtos.RequestDtos;
using Rent.Domain.Enums;

namespace Rent.Application.Dtos.ResponseDtos
{
    public record class ProductResponseDto
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public string Description { get; init; } = string.Empty;
        public decimal Price { get; init; }
        public int QuantityInStock { get; init; }
        public string ImageUrl { get; init; } = string.Empty;
        public DateTime CreatedAt { get; init; }
        public DateTime UpdatedAt { get; init; }
        public ProductType Type { get; init; }
        public ICollection<RentProductRequestDto> RentProducts { get; init; } = [];
    }
}
