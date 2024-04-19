namespace Modules.Document.Application.Dtos.ResponseDto
{
    public class HuntingLicenseResponseDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public UserResponseDto? User { get; set; }
        public string LicenseNumber { get; set; } = string.Empty;
    }
}
