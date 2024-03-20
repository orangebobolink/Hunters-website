using Identity.API.Configurations;
using Identity.API.Middleware;
using Identity.Infrastructure.Configurations;
using Identity.Services.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddFluentValidationConfiguration();
builder.Services.AddServicesConfiguration();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddRoutingConfiguration();
builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<ExceptionHandlingMiddleware>();
builder.Services.AddCorsConfiguration(builder.Configuration);
builder.Services.AddDatabaseConfiguration(builder.Configuration);
builder.Services.AddSwaggerGenConfiguration(builder.Configuration);
builder.Services.AddMassTransitConfiguration(builder.Configuration);
builder.Services.AddJWTAuthenticationConfiguration(builder.Configuration);
builder.Services.AddLoggerConfiguration(builder);
builder.Services.AddPagination();

var app = builder.Build();

app.UseCors();

app.UseMiddleware<ExceptionHandlingMiddleware>();

if(!app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI(s =>
    {
        s.SwaggerEndpoint("/swagger/v1/swagger.json", "Identity API");
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MigrateDatabase();

app.Run();
