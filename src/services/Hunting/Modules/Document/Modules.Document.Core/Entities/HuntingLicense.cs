namespace Modules.Document.Domain.Entities
{
    public class HuntingLicense
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public User? User { get; set; }
        public string LicenseNumber { get; set; } = string.Empty;
    }
}
