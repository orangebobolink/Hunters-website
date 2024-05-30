using Chat.API.Configurations;
using Chat.API.Hubs;
using Chat.API.Middleware;
using Chat.Infrastructure.Configurations;
using Chat.Services.Configurations;
using Identity.API.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSignalR();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddServicesConfiguration();
builder.Services.AddInfrastructureConfiguration();
builder.Services.AddTransient<ExceptionHandlingMiddleware>();
builder.Services.AddCorsConfiguration(builder.Configuration);
builder.Services.AddSwaggerGenConfiguration(builder.Configuration);
builder.Services.AddDatabaseConfiguration(builder.Configuration);
builder.Services.AddMassTransitConfiguration(builder.Configuration);
builder.Services.AddLoggerConfiguration(builder);

var app = builder.Build();

app.UseCors();

if (!app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI(s =>
    {
        s.SwaggerEndpoint("/swagger/v1/swagger.json", "Chat API");
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MigrateDatabase();

app.MapHub<ChatHub>("/chat");

app.Run();
