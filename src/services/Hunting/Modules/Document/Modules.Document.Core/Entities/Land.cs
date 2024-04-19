using Modules.Document.Domain.Interfaces;

namespace Modules.Document.Domain.Entities
{
    public class Land : IBaseEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
