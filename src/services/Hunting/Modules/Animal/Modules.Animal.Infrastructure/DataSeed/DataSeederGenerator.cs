using Modules.Animal.Domain.Entities;
using Modules.Animal.Domain.Enums;

namespace Modules.Animal.Infrastructure.DataSeed
{
    internal static class DataSeederGenerator
    {
        public static List<AnimalInfo> GetAnimalInfo()
        {
            List<AnimalInfo> animals = [
                new AnimalInfo()
                {
                    Id = Guid.NewGuid(),
                    Name = "Серый волк",
                    Description = "Вид хищных млекопитающих из семейства псовых (Canidae).",
                    Type = AnimalType.Mammal,
                    ImageUrl = "https://eger98.ru/d/volk.jpg",
                    HuntingSeasons = [
                         new HuntingSeason
                         {
                             Id = Guid.NewGuid(),
                             StartDate = new DateTime(2024, 11, 1),
                             EndDate = new DateTime(2024, 12, 31),
                             WayOfHunting = "С наскоком",
                             Weapon = "Гладкоствольное до 1000 Дж",
                             Note = "Охота с собаками"
                         },
                    ]
                },
                new AnimalInfo
                {
                    Id = Guid.NewGuid(),
                    Name = "Лось",
                    Description = "Род парнокопытных млекопитающих, самые крупные представители семейства оленевых.",
                    Type = AnimalType.Mammal,
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/8/8b/Moose_superior.jpg/800px-Moose_superior.jpg",
                    HuntingSeasons = [
                        new HuntingSeason
                        {
                            Id = Guid.NewGuid(),
                            StartDate = new DateTime(2024, 9, 1),
                            EndDate = new DateTime(2024, 11, 30),
                            WayOfHunting = "С собаками",
                            Weapon = "Арбалет",
                            Note = "Охота только до полудня"
                        }]
                }];

            return animals;
        }
    }
}