using Bogus;

namespace Identity.Services.Utilities
{
    internal static class RandomUsernameGeneratorUtility
    {
        public static string GenerateRandomUsername()
        {
            var faker = new Faker<string>().RuleFor(str => str,
                                                faker => faker.Person.UserName)
                                        .Generate();

            return faker!;
        }
    }
}
