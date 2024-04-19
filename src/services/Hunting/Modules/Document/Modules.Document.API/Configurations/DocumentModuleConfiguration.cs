using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modules.Document.Application.Configurations;
using Modules.Document.Infrastructure.Configurations;

namespace Modules.Document.API.Configurations
{
    public static class DocumentModuleConfiguration
    {
        public static void AddDocumentModuleConfiguration(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddApplicationConfiguration();
            services.AddInfrastructureConfiguration(configuration);
            //services.AddFluentValidationConfiguration();
        }
    }
}
