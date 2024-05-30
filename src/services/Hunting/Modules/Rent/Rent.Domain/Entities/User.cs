using Rent.Domain.Interfaces;

namespace Rent.Domain.Entities
{
    public class User : IBaseEntity
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string MiddleName { get; set; } = string.Empty;
        public ICollection<RentProduct> RentProducts { get; set; } = [];
    }
}
