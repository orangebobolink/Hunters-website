namespace Modules.Document.Application.Dtos.ResponseDtos
{
    public record CouponResponseDto
    {
        public Guid Id { get; init; }
        public Guid PermissionId { get; init; }
        public string AnimalName { get; init; } = string.Empty;
        public bool IsUsed { get; init; }
    }
}
