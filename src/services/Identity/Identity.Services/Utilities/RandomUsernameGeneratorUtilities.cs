using Bogus;

namespace Identity.Services.Utilities
{
    internal static class RandomUsernameGeneratorUtility
    {
        public static string GenerateRandomUsername()
        {
            var faker = new Faker();
            string username = faker.Person.UserName;

            return username;
        }
    }
}
