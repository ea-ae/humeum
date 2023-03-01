using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Infrastructure.Identity;

public class ProfileIdHandler : AuthorizationHandler<ProfileOwnershipRequirement> {
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ProfileOwnershipRequirement requirement) {
        if (context.Resource is HttpContext httpContext && httpContext.GetRouteValue("profile") is string profileId) {
            var claim = context.User.Claims.FirstOrDefault(claim => claim.Type == "profiles");
            string[]? profiles = claim?.Value.Split(",");

            if (profiles is not null && profiles.Contains(profileId)) {
                context.Succeed(requirement);
            }
        }

        return Task.CompletedTask;
    }
}
