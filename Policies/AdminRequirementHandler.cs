using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace Spotify.Policies;

public class AdminRequirementHandler : AuthorizationHandler<AdminRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AdminRequirement requirement)
    {
        if (context.User != null && context.User.HasClaim(c => c.Type == ClaimTypes.Role && c.Value == "Admin"))
            context.Succeed(requirement);
        return Task.CompletedTask;
    }
}