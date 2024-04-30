namespace Modules.Document.Application.Dtos.RequestDtos
{
    public class PermisionRequestDto
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string Number { get; set; } = string.Empty;
        public Guid AnimalId { get; set; }
        public AnimalRequestDto? Animal { get; set; }
        public Guid IssuedId { get; set; }
        public UserRequestDto? Issued { get; set; }
        public Guid ReceivedId { get; set; }
        public UserRequestDto? Received { get; set; }
        public List<CouponRequestDto> Coupons { get; set; } = [];
        public string Status { get; set; } = string.Empty;
    }
}
