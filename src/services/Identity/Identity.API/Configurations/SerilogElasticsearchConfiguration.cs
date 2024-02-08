using Serilog;
using Serilog.Exceptions;
using Serilog.Sinks.Elasticsearch;
using Serilog.Sinks.Network;

namespace Identity.API.Configurations
{
    public static class SerilogElasticsearchConfiguration
    {
        public static void AddLoggerConfiguration(this IServiceCollection services, WebApplicationBuilder builder)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment}.json", optional: true)
                .Build();

            Log.Logger = new LoggerConfiguration()
                .Enrich.WithExceptionDetails()
                .WriteTo.Console()
                .WriteTo.TCPSink("logstash", 5000)
                .Enrich.WithProperty("Environment", environment)
                .ReadFrom.Configuration(builder.Configuration)
                .CreateLogger();

            builder.Host.UseSerilog();
        }

        //private static ElasticsearchSinkOptions ConfigureElasticsearchSink(IConfigurationRoot configuration)
        //{
        //    return new ElasticsearchSinkOptions(new Uri(configuration["ElasticConfiguration:Uri"]!))
        //    {
        //        AutoRegisterTemplate = true,
        //        IndexFormat = $"logstash-{DateTime.Now:yyyy.MM.dd}",
        //        NumberOfReplicas = 1,
        //        NumberOfShards = 2
        //    };
        //}
    }
}
