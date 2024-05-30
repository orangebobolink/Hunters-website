namespace Shared.Messages.HunterLicenseMessage
{
    public class PaymentHuntingLicenseMessage
    {
        public string LicenseNumber { get; set; } = string.Empty;
        public bool IsPaid { get; set; }
    }
}
