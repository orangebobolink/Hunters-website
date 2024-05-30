namespace Rent.Application.Dtos.RequestDtos
{
    public class RentProductRequestDto
    {
        public Guid UserId { get; set; }
        public Guid ProductId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
