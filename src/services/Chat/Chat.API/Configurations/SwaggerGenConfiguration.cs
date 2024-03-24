using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;

namespace Identity.API.Configurations
{
    public static class SwaggerGenConfiguration
    {
        public static void AddSwaggerGenConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc(
                    configuration["SwaggerGen:Version"],
                    new OpenApiInfo
                    {
                        Title = configuration["SwaggerGen:Title"],
                        Version = configuration["SwaggerGen:Version"],
                        Contact = new OpenApiContact
                        {
                            Name = configuration["SwaggerGen:Contacts:Name"],
                            Email = configuration["SwaggerGen:Contacts:Email"],
                            Url = new Uri(configuration["SwaggerGen:Contacts:Url"]!),
                        },
                    }
                );

                options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = configuration["SwaggerGen:Description"],
                    Name = configuration["SwaggerGen:Name"],
                    Type = SecuritySchemeType.Http,
                    BearerFormat = configuration["SwaggerGen:BearerFormat"],
                    Scheme = JwtBearerDefaults.AuthenticationScheme,
                    Flows = new OpenApiOAuthFlows
                    {
                        Password = new OpenApiOAuthFlow
                        {
                            TokenUrl = new Uri(configuration["SwaggerGen:TokenUri"]!)
                        }
                    }
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

                options.AddSignalRSwaggerGen();
            });
        }
    }
}
