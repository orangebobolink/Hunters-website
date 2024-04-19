namespace Modules.Document.Application.Dtos.RequestDtos
{
    public class HuntingLicenseRequestDto
    {
        public Guid UserId { get; set; }
        public UserRequestDto? User { get; set; }
        public string LicenseNumber { get; set; } = string.Empty;
    }
}
