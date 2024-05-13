using Microsoft.Extensions.DependencyInjection;
using Rent.Application.Interfaces;
using Rent.Application.Services;

namespace Rent.Application.Configurations
{
    public static class ApplicationConfiguration
    {
        public static void AddApplicationConfiguration(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IRentProductService, RentProductService>();
        }
    }
}
