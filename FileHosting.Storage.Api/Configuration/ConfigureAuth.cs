using System.IdentityModel.Tokens.Jwt;
using FileHosting.Storage.Api.Consts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace FileHosting.Storage.Api.Configuration;

public static class ConfigureAuth
{
    public static IServiceCollection AddAuth(this IServiceCollection services)
    {
        JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.Authority = "https://localhost:5001";
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false,
                    NameClaimType = JwtRegisteredClaimNames.Sub
                };
            });
        services.AddAuthorization(options =>
        {
            options.AddPolicy(Policy.Authorized, policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.RequireClaim("scope", "FileHosting.Storage.Api");
            });
        });

        return services;
    }
}