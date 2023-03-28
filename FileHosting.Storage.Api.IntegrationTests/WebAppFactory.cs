using FileHosting.Shared.Api.IntegrationTests;
using FileHosting.Shared.AppCore.UserAccessor;
using FileHosting.Storage.Api.Consts;
using FileHosting.Storage.AppCore.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FileHosting.Storage.Api.IntegrationTests;

public class WebAppFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services.AddScoped<IUserAccessor, TestUserAccessor>();
            services.AddSingleton<IStorageService, TestStorageService>();

            services.AddAuthorization(options =>
            {
                options.AddPolicy(Policy.Authorized, policy => policy.AddRequirements(new NothingAuthorizationRequirement()));
            });
        });
    }

    protected override IHost CreateHost(IHostBuilder builder)
    {
        builder.UseEnvironment("test");
        return base.CreateHost(builder);
    }
}