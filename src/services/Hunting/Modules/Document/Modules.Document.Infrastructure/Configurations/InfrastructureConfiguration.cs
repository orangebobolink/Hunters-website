using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modules.Document.Domain.Interfaces;
using Modules.Document.Infrastructure.Contexts;
using Modules.Document.Infrastructure.Repositories;
using Shared.Infrastructure.Configurations;

namespace Modules.Document.Infrastructure.Configurations
{
    public static class InfrastructureConfiguration
    {
        public static void AddInfrastructureConfiguration(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDatabaseConfiguration<DocumentDbContext>(configuration);
            services.AddScoped<IAnimalRepository, AnimalRepository>();
            services.AddScoped<IHuntingLicenseRepository, HuntingLicenseRepository>();
            services.AddScoped<IFeedingRepository, FeedingRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICouponRepository, CouponRepository>();
            services.AddScoped<IFeedingProductRepository, FeedingProductRepository>();
            services.AddScoped<ILandRepository, LandRepository>();
            services.AddScoped<IRaidRepository, RaidRepository>();
            services.AddScoped<ITripParticipantRepository, TripParticipantRepository>();
            services.AddScoped<ITripRepository, TripRepository>();
            services.AddScoped<IPermissionForExtractionOfHuntingAnimalRepository,
                PermissionForExtractionOfHuntingAnimalRepository>();
        }
    }
}
