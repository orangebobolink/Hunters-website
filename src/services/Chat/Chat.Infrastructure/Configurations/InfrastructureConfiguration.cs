using Chat.Infrastructure.Repositories;
using Chat.Interfaces.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Chat.Infrastructure.Configurations
{
    public static class InfrastructureConfiguration
    {
        public static void AddInfrastructureConfiguration(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IGroupRepository, GroupRepository>();
            services.AddScoped<IMessageRepository, MessageRepository>();
        }
    }
}
