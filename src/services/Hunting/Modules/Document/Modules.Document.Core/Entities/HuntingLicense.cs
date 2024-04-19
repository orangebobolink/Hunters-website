using Modules.Document.Domain.Interfaces;

namespace Modules.Document.Domain.Entities
{
    public class HuntingLicense : IBaseEntity
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public User? User { get; set; }
        public string LicenseNumber { get; set; } = string.Empty;
    }
}
