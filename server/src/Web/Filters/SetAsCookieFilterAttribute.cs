using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Web.Filters;

/// <summary>
/// Sets the returned string as a cookie.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
internal class SetAsCookieFilterAttribute : Attribute, IActionFilter {
    /// <summary>Name of the cookie that the string will be assigned to.</summary>
    public required string CookieName { get; init; }

    /// <summary>Overridable cookie options.</summary>
    public CookieOptions CookieOptions { get; init; } = new CookieOptions() {
        HttpOnly = true,
        Expires = DateTime.UtcNow.AddDays(90)
    };

    public void OnActionExecuting(ActionExecutingContext context) { }

    public void OnActionExecuted(ActionExecutedContext context) {
        if (context.Result is ObjectResult result && result.Value is string cookie) {
            context.HttpContext.Response.Cookies.Append(CookieName, cookie, CookieOptions);
            context.Result = new NoContentResult();
        }
    }
}
