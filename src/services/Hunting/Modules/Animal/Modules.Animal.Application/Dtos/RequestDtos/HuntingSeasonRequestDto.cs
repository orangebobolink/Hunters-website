using Modules.Animal.Domain.Entities;

namespace Modules.Animal.Application.Dtos.RequestDtos
{
    public class HuntingSeasonRequestDto
    {
        public Guid AnimalId { get; set; }
        public AnimalInfo? Animal { get; set; }
        public DateTime StartDate { get; set; } = DateTime.MinValue;
        public DateTime EndDate { get; set; } = DateTime.MinValue;
        public string WayOfHunting { get; set; } = string.Empty;
        public string Weapon { get; set; } = string.Empty;
        public string Note { get; set; } = string.Empty;
    }
}
