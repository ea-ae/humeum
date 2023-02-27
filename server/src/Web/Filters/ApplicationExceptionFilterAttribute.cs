using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Common.Exceptions;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Web.Filters;

/// <summary>
/// Handles application exceptions and converts them to appropriate HTTP responses.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class ApplicationExceptionFilterAttribute : ExceptionFilterAttribute {
    /// <summary>
    /// Called when application services (commands/queries) in controller actions throw unhandled exceptions.
    /// </summary>
    public override void OnException(ExceptionContext context) {
        if (context.Exception is NotFoundValidationException) {
            HandleException(context, "Not Found", StatusCodes.Status404NotFound);
        } else if (context.Exception is ConflictValidationException) {
            HandleException(context, "Validation Error", StatusCodes.Status409Conflict);
        } else if (context.Exception is ApplicationValidationException or DomainException) {
            HandleException(context, "Validation Error", StatusCodes.Status400BadRequest);
        } else if (context.Exception is IAuthenticationException) {
            HandleException(context, "Authentication Error", StatusCodes.Status401Unauthorized);
        }
    }

    /// <summary>
    /// Helper method that generates an error response and marks the exception as handled.
    /// </summary>
    static void HandleException(ExceptionContext context, string title, int statusCode) {
        var error = new ProblemDetails {
            Title = title,
            Detail = context.Exception.Message,
            Status = statusCode
        };

        context.Result = new ObjectResult(error) {
            StatusCode = statusCode
        };

        context.ExceptionHandled = true;
    }
}
