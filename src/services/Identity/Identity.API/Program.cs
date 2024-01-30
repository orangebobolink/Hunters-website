using Identity.API.Configurations;
using Identity.API.Middleware;
using Identity.Infrastructure.Configurations;
using Identity.Services.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddCorsConfiguration(builder.Configuration);
builder.Services.AddServicesConfiguration();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddTransient<ExceptionHandlingMiddleware>();
builder.Services.AddDatabaseConfiguration(builder.Configuration);
builder.Services.AddSwaggerGenConfiguration();
builder.Services.AddJWTAuthenticationConfiguration(builder.Configuration);

var app = builder.Build();

app.UseCors("CorsPolicy");

app.UseMiddleware<ExceptionHandlingMiddleware>();

if(!app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI(s =>
    {
        s.SwaggerEndpoint("/swagger/v1/swagger.json", "Demo API v1");
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MigrateDatabase();

app.Run();
