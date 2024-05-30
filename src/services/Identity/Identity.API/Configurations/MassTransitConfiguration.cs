using MassTransit;
using Identity.Infrastructure.Extensions;
using System.Reflection;
using Identity.Services.Consumers;

namespace Identity.API.Configurations
{
    internal static class MassTransitConfiguration
    {
        public static void AddMassTransitConfiguration(
            this IServiceCollection services,
            ConfigurationManager config)
        {
            services.AddMassTransit(x =>
            {
                var assembly = Assembly.GetAssembly(
                    typeof(PaymentHuntingLicenseConsumer));
                var host = config["RabbitMQ:Host"];
                var virtualHost = config["RabbitMQ:VirtualHost"];
                var username = config["RabbitMQ:Username"];
                var password = config["RabbitMQ:Password"];

                x.AddEntityFrameworkOutboxPattern();
                x.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter("Identity", false));
                x.AddConsumers(assembly);

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(host, virtualHost, h =>
                    {
                        h.Username(username);
                        h.Password(password);
                    });

                    cfg.ConfigureEndpoints(context);
                });
            });
        }
    }
}
