using Shared.Infrastructure.Extensions;
using Modules.Animal.API.Configurations;
using Modules.Animal.Infrastructure.Configurations;
using Hunting.API.Configurations;
using Modules.Document.API.Configurations;
using Hunting.API.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAnimalModuleConfiguration(builder.Configuration);
builder.Services.AddDocumentModuleConfiguration(builder.Configuration);
builder.Services.AddJwtAuthenticationConfiguration(builder.Configuration);
builder.Services.AddSharedInfrastructure(builder.Configuration);
builder.Services.AddSwaggerGenConfiguration(builder.Configuration);
builder.Services.AddRedisConfiguration(builder.Configuration);
builder.Services.AddMassTransitConfiguration(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddTransient<ExceptionHandlingMiddleware>();

var app = builder.Build();

app.UseCors();

if (!app.Environment.IsProduction())
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