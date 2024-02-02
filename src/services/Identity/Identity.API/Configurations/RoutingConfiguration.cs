﻿namespace Identity.API.Configurations
{
    public static class RoutingConfiguration
    {
        public static void AddRoutingConfiguration(this IServiceCollection services)
        {
            services.AddRouting(options => options.LowercaseUrls = true);
        }
    }
}
