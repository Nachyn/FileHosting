using EventBus.Kafka;
using FileHosting.Shared.Api.Middlewares;
using FileHosting.Shared.AppCore.UserAccessor;
using FileHosting.Storage.Api.Configuration;
using FileHosting.Storage.Api.UserAccessor;
using FileHosting.Storage.AppCore;
using FileHosting.Storage.Infrastructure;
using FileHosting.Storage.Infrastructure.Data;
using FluentValidation;
using MinimalApi.Endpoint.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
    $"appsettings.{builder.Environment.EnvironmentName}.json"));

// Add services to the container.

builder.Services.AddEndpoints();
builder.Services.AddAuth();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddKafkaEventBus(builder.Configuration);
builder.Services.ConfigureStorageAppCore();
builder.Services.ConfigureStorageInfrastructure(builder.Configuration, Path.Combine(builder.Environment.ContentRootPath, "files"));
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IUserAccessor, UserAccessor>();
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    try
    {
        var storageContext = scope.ServiceProvider.GetRequiredService<StorageContext>();
        await StorageContextSeed.SeedAsync(storageContext);
    }
    catch (Exception exception)
    {
        app.Logger.LogError(exception, "An error occurred seeding the DB.");
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<ExceptionMiddleware>();

app.MapEndpoints();

app.Run();

public partial class Program
{
}