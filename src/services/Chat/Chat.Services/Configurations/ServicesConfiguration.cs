using Chat.Services.Interfaces;
using Chat.Services.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Chat.Services.Configurations
{
    public static class ServicesConfiguration
    {
        public static void AddServicesConfiguration(this IServiceCollection services)
        {
            services.AddScoped<IGroupService, GroupService>();
            services.AddScoped<IMessageService, MessageService>();
        }
    }
}
