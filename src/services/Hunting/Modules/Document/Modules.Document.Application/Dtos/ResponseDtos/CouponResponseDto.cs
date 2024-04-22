namespace Modules.Document.Application.Dtos.ResponseDtos
{
    public record class CouponResponseDto
    {
        public Guid Id { get; init; }
        public Guid PermissionId { get; init; }
        public string AnimalName { get; init; } = string.Empty;
    }
}
