namespace Modules.Animal.Domain.Helpers
{
    public static class ErrorMessageHelper
    {
        public static string AnimalAlreadyExists(string name)
            => $"An animal with the name '{name}' already exists.";

        public static string AnimalNotFound(Guid id)
            => $"An animal with the Id '{id}' not found in the database.";

        public static string NoAnimalsFound()
            => "No animals found in the database.";
    }
}
