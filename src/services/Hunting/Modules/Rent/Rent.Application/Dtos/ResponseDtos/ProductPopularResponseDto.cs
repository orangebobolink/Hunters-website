namespace Rent.Application.Dtos.ResponseDtos
{
    public class ProductPopularResponseDto
    {
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int RentedQuantity { get; set; }
    }
}
