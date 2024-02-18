using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;

namespace Identity.API.Configurations
{
    public static class SwaggerGenConfiguration
    {
        public static void AddSwaggerGenConfiguration(this IServiceCollection services, IConfiguration congiguration)
        {
            services.AddSwaggerGen(options =>
            {
                options.ConfigurePagination();

                options.SwaggerDoc(
                    congiguration["SwaggerGen:Version"],
                    new OpenApiInfo
                    {
                        Title = congiguration["SwaggerGen:Title"],
                        Version = congiguration["SwaggerGen:Version"],
                        Contact = new OpenApiContact
                        {
                            Name = congiguration["SwaggerGen:Contacts:Name"],
                            Email = congiguration["SwaggerGen:Contacts:Email"],
                            Url = new Uri(congiguration["SwaggerGen:Contacts:Url"]!),
                        },
                    }
                );

                options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = congiguration["SwaggerGen:Description"],
                    Name = congiguration["SwaggerGen:Name"],
                    Type = SecuritySchemeType.Http,
                    BearerFormat = congiguration["SwaggerGen:BearerFormat"],
                    Scheme = JwtBearerDefaults.AuthenticationScheme
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = JwtBearerDefaults.AuthenticationScheme
                            }
                        },
                        new string[]{}
                    }
                });
            });
        }
    }
}
