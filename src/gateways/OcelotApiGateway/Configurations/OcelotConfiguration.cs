namespace OcelotApiGateway.Configurations
{
    public static class OcelotConfiguration
    {
        public static void AddOcelotConfiguration(this IConfigurationBuilder builder)
        {
            var enviroment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            builder.AddJsonFile($"ocelot.{enviroment}.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();
        }
    }
}
