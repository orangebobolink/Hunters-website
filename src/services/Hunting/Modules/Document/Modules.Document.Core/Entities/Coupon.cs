using Modules.Document.Domain.Interfaces;

namespace Modules.Document.Domain.Entities
{
    public class Coupon : IBaseEntity
    {
        public Guid Id { get; set; }
        public Guid PermissionId { get; set; }
        public PermissionForExtractionOfHuntingAnimal? Permission { get; set; }
        public string AnimalName { get; set; } = string.Empty;
        public bool IsUsed { get; set; } = false;
    }
}
