namespace Modules.Document.Application.Dtos.ResponseDtos
{
    public record class HuntingLicenseResponseDto
    {
        public Guid Id { get; init; }
        public Guid UserId { get; init; }
        public UserResponseDto? User { get; init; }
        public string LicenseNumber { get; init; } = string.Empty;
    }
}
