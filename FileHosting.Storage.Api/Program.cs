using FileHosting.Storage.AppCore.Interfaces;
using FileHosting.Storage.Infrastructure;
using FileHosting.Storage.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureStorageInfrastructure(builder.Configuration);
builder.Services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
builder.Services.AddScoped(typeof(IReadRepository<>), typeof(EfRepository<>));

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

app.MapControllers();

app.Run();