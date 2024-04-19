using Modules.Document.Domain.Enums;
using Modules.Document.Domain.Interfaces;

namespace Modules.Document.Domain.Entities
{
    public class Animal : IBaseEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public AnimalType Type { get; set; }
    }
}
