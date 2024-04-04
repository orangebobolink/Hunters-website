using Modules.Animal.Domain.Enums;

namespace Modules.Animal.Domain.Entities
{
    public class AnimalInfo
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string LatinName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public AnimalType Type { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public List<HuntingSeason> HuntingSeasons { get; set; } = [];
    }
}
