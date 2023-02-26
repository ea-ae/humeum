using Application.Common.Exceptions;

using Domain.Common.Exceptions;

using Microsoft.AspNetCore.Http.HttpResults;
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
            var error = new ProblemDetails {
                Title = "Not Found",
                Detail = context.Exception.Message,
                Status = StatusCodes.Status404NotFound
            };

            context.Result = new ObjectResult(error) {
                StatusCode = StatusCodes.Status404NotFound
            };
            context.ExceptionHandled = true;
        } else if (context.Exception is ConflictValidationException) {
            var error = new ProblemDetails {
                Title = "Validation Error",
                Detail = context.Exception.Message,
                Status = StatusCodes.Status409Conflict
            };

            context.Result = new ObjectResult(error) {
                StatusCode = StatusCodes.Status409Conflict
            };
            context.ExceptionHandled = true;
        } else if (context.Exception is ApplicationValidationException or DomainException) {
            var error = new ProblemDetails {
                Title = "Validation Error",
                Detail = context.Exception.Message,
                Status = StatusCodes.Status400BadRequest
            };

            context.Result = new ObjectResult(error) {
                StatusCode = StatusCodes.Status400BadRequest
            };
            context.ExceptionHandled = true;
        } else if (context.Exception is IAuthenticationException) {
            var error = new ProblemDetails {
                Title = "Authentication Error",
                Detail = context.Exception.Message,
                Status = StatusCodes.Status401Unauthorized
            };

            context.Result = new ObjectResult(error) {
                StatusCode = StatusCodes.Status401Unauthorized
            };
        }
    }
}
