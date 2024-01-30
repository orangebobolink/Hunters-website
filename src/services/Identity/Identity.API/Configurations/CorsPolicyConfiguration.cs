namespace Identity.API.Configurations
{
    public static class CorsPolicyConfiguration
    {
        public static void ConfigureCors(this IServiceCollection services, IConfiguration configuration) =>
           services.AddCors(options =>
           {
               options.AddPolicy("CorsPolicy", builder =>
                  builder.AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()
                    .WithOrigins(configuration.GetSection("CorsPolicy:AllowedOrigins").Value!));
           });
    }
}
