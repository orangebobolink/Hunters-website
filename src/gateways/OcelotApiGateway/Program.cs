using Microsoft.OpenApi.Models;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using OcelotApiGateway.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddOcelotConfiguration();

builder.Services.AddOcelot(builder.Configuration);
builder.Services.AddSwaggerForOcelot(builder.Configuration);
builder.Services.AddCorsConfiguration(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSignalRSwaggerGen();
});

var app = builder.Build();

app.UseCors();

app.UseRouting();

app.UseSwaggerForOcelotUI(options =>
    options.PathToSwaggerGenerator = "/swagger/docs"
    );

app.UseWebSockets();

await app.UseOcelot();

app.Run();
