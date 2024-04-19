namespace Shared.Messages.HunterLicenseMessage
{
    public class CreateHuntingLicense
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string LicenseNumber { get; set; } = string.Empty;
        public DateTime IssuedDate { get; set; } = DateTime.MinValue;
        public DateTime ExpiryDate { get; set; } = DateTime.MinValue;
    }
}
