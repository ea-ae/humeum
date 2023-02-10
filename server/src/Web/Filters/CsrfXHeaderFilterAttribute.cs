using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Web.Filters;

/// <summary>
/// This resource filter can be applied onto controllers or controller actions to demand
/// that a certain X-Header exists for the request to be accepted. This is a useful
/// CSRF-prevention measure, because X-Headers can only be set through same-origin AJAX,
/// unless special CORS access has been provided.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
internal class CsrfXHeaderFilterAttribute : Attribute, IResourceFilter {
    /// <summary>Name of CSRF-check header.</summary>
    public string XHeaderName { get; init; } = "X-Requested-With";

    /// <summary>Whether to exempt GET requests from CSRF checks.</summary>
    public bool AllowGet { get; init; } = true;

    public void OnResourceExecuting(ResourceExecutingContext context) {
        if (!AllowGet || context.HttpContext.Request.Method != "GET") {
            if (!context.HttpContext.Request.Headers.TryGetValue(XHeaderName, out _)) {
                // CSRF header missing, short-circuit pipeline
                context.Result = new StatusCodeResult(StatusCodes.Status403Forbidden);
            }
        }
    }

    public void OnResourceExecuted(ResourceExecutedContext context) { }
}
