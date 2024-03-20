using Identity.Infrastructure.Contexts;
using MassTransit;

namespace Identity.Infrastructure.Extensions
{
    public static class MassTransitExtensions
    {
        public static void AddEntityFrameworkOutboxPattern(this IBusRegistrationConfigurator configurator)
        {
            configurator.AddEntityFrameworkOutbox<ApplicationDbContext>(o =>
            {
                o.UseSqlServer();

                o.QueryDelay = TimeSpan.FromSeconds(1);

                o.UseBusOutbox();
            });
        }
    }
}
