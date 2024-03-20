using MassTransit;
using Shared.Messages.UserMessages;
using System.Reflection;
using Identity.Infrastructure.Extensions;

namespace Identity.API.Configurations
{
    internal static class MassTransitConfiguration
    {
        public static void AddMassTransitConfiguration(this IServiceCollection services, ConfigurationManager config)
        {
            services.AddMassTransit(x =>
            {
                var assembly = Assembly.GetAssembly(typeof(CreateUserMessage));
                var host = config["RabbitMQ:Host"];
                var virtualHost = config["RabbitMQ:VirtualHost"];
                var username = config["RabbitMQ:Username"];
                var password = config["RabbitMQ:Password"];

                x.AddEntityFrameworkOutboxPattern();
                x.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter("Chat", false));
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
