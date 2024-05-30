using Hunting.Bus.Consumers;
using MassTransit;
using System.Reflection;

namespace Hunting.API.Configurations
{
    public static class MassTransitConfiguration
    {
        public static void AddMassTransitConfiguration(this IServiceCollection services, ConfigurationManager config)
        {
            services.AddMassTransit(x =>
            {
                var assembly = Assembly.GetAssembly(typeof(UserDataSeedConsumer));
                var host = config["RabbitMQ:Host"];
                var virtualHost = config["RabbitMQ:VirtualHost"];
                var username = config["RabbitMQ:Username"];
                var password = config["RabbitMQ:Password"];

                //x.AddEntityFrameworkOutbox<ApplicationDbContext>(o =>
                //{
                //    o.UseSqlServer();

                //    o.QueryDelay = TimeSpan.FromSeconds(1);

                //    o.UseBusOutbox();
                //});

                x.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter("Hunting", false));
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
