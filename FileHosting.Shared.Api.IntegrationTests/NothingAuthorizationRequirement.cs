using Microsoft.AspNetCore.Authorization;

namespace FileHosting.Shared.Api.IntegrationTests;

public class NothingAuthorizationRequirement : AuthorizationHandler<NothingAuthorizationRequirement>, IAuthorizationRequirement
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, NothingAuthorizationRequirement requirement)
    {
        context.Succeed(requirement);
        return Task.CompletedTask;
    }
}