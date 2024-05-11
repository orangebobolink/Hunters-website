namespace Rent.Application.Dtos.RequestDtos
{
    public class RentProductRequestDto
    {
        public Guid UserId { get; set; }
        public RentUserRequestDto User { get; set; } = null!;
        public Guid ProductId { get; set; }
        public ProductRequestDto Product { get; set; } = null!;
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
