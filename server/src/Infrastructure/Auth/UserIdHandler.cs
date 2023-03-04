using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Infrastructure.Auth;

/// <summary>
/// Checks whether the user ID in the route ("{user}") matches the user ID ("uid") in the user claims.
/// </summary>
public class UserIdHandler : AuthorizationHandler<UserOwnershipRequirement> {
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, UserOwnershipRequirement requirement) {
        if (context.Resource is HttpContext httpContext) {
            if (httpContext.GetRouteValue("user") is string userId && context.User.HasClaim("uid", userId)) {
                context.Succeed(requirement);
            }
        }

        return Task.CompletedTask;
    }
}
