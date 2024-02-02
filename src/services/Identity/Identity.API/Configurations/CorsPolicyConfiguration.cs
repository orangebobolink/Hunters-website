namespace Identity.API.Configurations
{
    public static class CorsPolicyConfiguration
    {
        public static void AddCorsConfiguration(this IServiceCollection services, IConfiguration configuration) =>
           services.AddCors(options =>
           {
               options.AddPolicy("CorsPolicy", builder =>
                  builder.AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()
                    .WithOrigins(configuration["CorsPolicy:AllowedOrigins"]!));
           });
    }
}