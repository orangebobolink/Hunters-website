namespace Identity.Domain.Entities
{
    public class HuntingLicense
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public User? User { get; set; }
        public string LicenseNumber { get; set; } = string.Empty;
        public DateTime IssuedDate { get; set; } = DateTime.MinValue;
        public DateTime ExpiryDate { get; set; } = DateTime.MinValue;
        public bool IsPaid { get; set; } = false;
    }
}
