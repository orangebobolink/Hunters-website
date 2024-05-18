using Modules.Document.Domain.Interfaces;

namespace Modules.Document.Domain.Entities
{
    public class User : IBaseEntity
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string MiddleName { get; set; } = string.Empty;
        public ICollection<Raid> Raids { get; set; } = [];
        public ICollection<Trip> Trips { get; set; } = [];
    }
}