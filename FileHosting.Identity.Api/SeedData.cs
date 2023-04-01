using FileHosting.Identity.Api.Consts;
using FileHosting.Identity.Api.Data;
using FileHosting.Identity.Api.Events;
using FileHosting.Identity.Api.Models;
using FileHosting.Identity.Api.Settings;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Serilog;

namespace FileHosting.Identity.Api;

public static class SeedData
{
    public static async Task EnsureSeedData(WebApplication app)
    {
        using var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var eventPublisher = scope.ServiceProvider.GetRequiredService<IPublisher>();
        var adminSettings = scope.ServiceProvider.GetRequiredService<IOptions<AdminSettings>>().Value;
        if (adminSettings is null)
        {
            throw new InvalidOperationException(nameof(adminSettings));
        }

        await context.Database.MigrateAsync();

        await using (var transaction = await context.Database.BeginTransactionAsync())
        {
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

            var isRolesNotCreated = !await roleManager.Roles.AnyAsync();
            if (isRolesNotCreated)
            {
                var adminRole = new ApplicationRole
                {
                    Name = ApplicationRoles.Admin
                };
                var result = await roleManager.CreateAsync(adminRole);
                CheckIdentityResult(result);
            }

            var admin = await userManager.FindByNameAsync(adminSettings.UserName);
            if (admin == null)
            {
                admin = new ApplicationUser
                {
                    UserName = adminSettings.UserName,
                    Email = adminSettings.Email,
                    EmailConfirmed = true
                };
                var result = await userManager.CreateAsync(admin, adminSettings.Password);
                CheckIdentityResult(result);

                result = await userManager.AddToRoleAsync(admin, ApplicationRoles.Admin);
                CheckIdentityResult(result);

                await eventPublisher.Publish(new UserCreatedEvent
                {
                    UserId = admin.Id,
                    UserName = admin.UserName,
                    Email = admin.Email
                });
                Log.Debug("admin created");
            }
            else
            {
                Log.Debug("admin already exists");
            }

            await transaction.CommitAsync();
        }
    }

    private static void CheckIdentityResult(IdentityResult result)
    {
        if (!result.Succeeded)
        {
            throw new Exception(result.Errors.First().Description);
        }
    }
}