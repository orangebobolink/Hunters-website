using MassTransit;
using System.Reflection;
using Chat.Infrastructure.Extensions;
using Chat.Services.MassTransit.Consumers;

namespace Chat.API.Configurations
{
    internal static class MassTransitConfiguration
    {
        public static void AddMassTransitConfiguration(this IServiceCollection services, ConfigurationManager config)
        {
            services.AddMassTransit(x =>
            {
                var assembly = Assembly.GetAssembly(typeof(CreateUserConsumer));
                var host = config["RabbitMQ:Host"];
                var virtualHost = config["RabbitMQ:VirtualHost"];
                var username = config["RabbitMQ:Username"];
                var password = config["RabbitMQ:Password"];

                x.AddEntityFrameworkOutboxPattern();
                x.SetKebabCaseEndpointNameFormatter();
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
