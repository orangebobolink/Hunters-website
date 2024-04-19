using Microsoft.Extensions.DependencyInjection;
using Modules.Document.Application.Interfaces;
using Modules.Document.Application.Services;

namespace Modules.Document.Application.Configurations
{
    public static class ApplicationConfiguration
    {
        public static void AddApplicationConfiguration(this IServiceCollection services)
        {
            services.AddScoped<ITripService, TripService>();
            services.AddScoped<ITripParticipantService, TripParticipantService>();
            services.AddScoped<ICouponService, CouponService>();
            services.AddScoped<IFeedingProductService, FeedingProductService>();
            services.AddScoped<IFeedingService, FeedingService>();
            services.AddScoped<ILandService, LandService>();
            services.AddScoped<IPermissionService, PermissionService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IRaidService, RaidService>();
        }
    }
}
