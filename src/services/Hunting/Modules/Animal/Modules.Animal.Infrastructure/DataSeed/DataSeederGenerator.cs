using Modules.Animal.Domain.Entities;
using Modules.Animal.Domain.Enums;

namespace Modules.Animal.Infrastructure.DataSeed
{
    internal class DataSeederGenerator
    {
        public static List<AnimalInfo> GetAnimalInfo()
        {
            List<AnimalInfo> animals = [
                new AnimalInfo()
                {  
                    Id = Guid.NewGuid(),
                    Name = "Gray Wolf",
                    Description = "The gray wolf, also known as the timber wolf, " +
                                "is a canine native to the wilderness and remote areas of Eurasia and North America.",
                    Type = AnimalType.Mammal,
                    ImageUrl = "https://example.com/gray-wolf.jpg",
                    HuntingSeasons = [
                         new HuntingSeason
                         {
                             Id = Guid.NewGuid(),
                             StartDate = new DateTime(2023, 11, 1),
                             EndDate = new DateTime(2023, 12, 31),
                             WayOfHunting = "Stalking",
                             Weapon = "Rifle",
                             Note = "Hunting wolf requires a special permit."
                         },
                    ]
                },
                new AnimalInfo
                {
                    Id = Guid.NewGuid(),
                    Name = "Elk",
                    Description = "The elk or wapiti is one of the largest species within the deer family " +
                                 "and is native to North America and Eastern Asia.",
                    Type = AnimalType.Mammal,
                    ImageUrl = "https://example.com/elk.jpg",
                    HuntingSeasons = [
                        new HuntingSeason
                        {
                            Id = Guid.NewGuid(),
                            StartDate = new DateTime(2023, 9, 1),
                            EndDate = new DateTime(2023, 11, 30),
                            WayOfHunting = "Calling",
                            Weapon = "Bow",
                            Note = "Elk hunting is popular during the rut season."
                        }]
                }];

            return animals;
        }
    }
}
