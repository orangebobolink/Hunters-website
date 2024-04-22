using Modules.Document.Domain.Interfaces;

namespace Modules.Document.Domain.Entities
{
    public class Raid : IBaseEntity
    {
        public Guid Id { get; set; }
        public DateTime ExitTime { get; set; }
        public DateTime ReturnedTime { get; set; }
        public ICollection<User> Participants { get; set; } = [];
        public string Note { get; set; } = string.Empty;
        public Guid LandId { get; set; }
        public Land? Land { get; set; }
    }
}
