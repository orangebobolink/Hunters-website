using Modules.Animal.Domain.Entities;

namespace Modules.Animal.Domain.Helpers
{
    public static class CacheHelper
    {
        public static string GetCacheKeyForAnimal(Guid id)
        {
            var key = nameof(AnimalInfo) + id;

            return key;
        }

        public static string GetCacheKeyForAllAnimals()
        {
            var key = nameof(AnimalInfo);

            return key;
        }
    }
}
