using Modules.Document.Domain.Enums;

namespace Modules.Document.Domain.Entities
{
    public class Animal
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public AnimalType Type { get; set; }
    }
}
