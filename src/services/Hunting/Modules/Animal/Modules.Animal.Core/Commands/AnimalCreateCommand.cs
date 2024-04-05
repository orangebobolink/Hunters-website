using MediatR;
using Modules.Animal.Domain.Entities;
using Modules.Animal.Domain.Enums;

namespace Modules.Animal.Domain.Commands
{
    public class AnimalCreateCommand : IRequest<AnimalInfo>
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public AnimalType Type { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
    }
}
