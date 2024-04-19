namespace Chat.API.Configurations
{
    public static class CorsPolicyConfiguration
    {
        public static void AddCorsConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder => builder.AllowAnyMethod()
                                 .AllowAnyHeader()
                                 .AllowCredentials()
                                 .WithOrigins(configuration["CorsPolicy:AllowedOrigins"]!
                ));
            });
        }
    }
}