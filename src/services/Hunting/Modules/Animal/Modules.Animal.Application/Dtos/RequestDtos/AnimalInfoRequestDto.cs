using Modules.Animal.Domain.Enums;

namespace Modules.Animal.Application.Dtos.RequestDtos
{
    public class AnimalInfoRequestDto
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public AnimalType Type { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
    }
}
