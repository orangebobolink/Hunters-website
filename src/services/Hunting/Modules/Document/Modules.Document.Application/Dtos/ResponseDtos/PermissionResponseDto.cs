namespace Modules.Document.Application.Dtos.ResponseDtos
{
    public record class PermissionResponseDto
    {
        public Guid Id { get; init; }
        public DateTime FromDate { get; init; }
        public DateTime ToDate { get; init; }
        public string Number { get; init; } = string.Empty;
        public Guid AnimalId { get; init; }
        public AnimalResponseDto? Animal { get; init; }
        public Guid IssuedId { get; init; }
        public UserResponseDto? Issued { get; init; }
        public Guid ReceivedId { get; init; }
        public UserResponseDto? Received { get; init; }
        public List<CouponResponseDto> Coupons { get; init; } = [];
        public Guid LandId { get; init; }
        public LandResponseDto? Land { get; init; }
    }
}
