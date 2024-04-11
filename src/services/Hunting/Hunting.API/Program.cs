using Shared.Infrastructure.Extensions;
using Modules.Animal.API.Configurations;
using Identity.API.Configurations;
using Modules.Animal.Infrastructure.Configurations;
using Hunting.API.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAnimalModuleConfiguration(builder.Configuration);
builder.Services.AddJWTAuthenticationConfiguration(builder.Configuration);
builder.Services.AddSharedInfrastructure(builder.Configuration);
builder.Services.AddSwaggerGenConfiguration(builder.Configuration);
builder.Services.AddRedisConfiguration(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.UseCors();

if(!app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI(s =>
    {
        s.SwaggerEndpoint("/swagger/v1/swagger.json", "Hunting API");
    });
}

app.UseHttpsRedirection();

app.MapControllers();

app.UseAuthentication();
app.UseAuthorization();

app.MigrateDatabase();

app.Run();