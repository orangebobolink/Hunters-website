namespace Rent.Application.Dtos.ResponseDtos
{
    public record class RentProductResponseDto
    {
        public Guid Id { get; init; }
        public Guid UserId { get; init; }
        public RentUserResponseDto User { get; init; } = null!;
        public Guid ProductId { get; init; }
        public ProductResponseDto Product { get; init; } = null!;
        public DateTime FromDate { get; init; }
        public DateTime ToDate { get; init; }
        public string Status { get; init; } = string.Empty;
    }
}
