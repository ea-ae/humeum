using Domain.Common.Interfaces;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Web.Filters;

/// <summary>
/// Handles result objects by either unwrapping them or returning HTTP error codes.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class ApplicationResultFilterAttribute : ResultFilterAttribute {
    /// <summary>
    /// Unwrap the result before execution or transform it to an HTTP error.
    /// </summary>
    /// <param name="context">Filter context.</param>
    public override void OnResultExecuting(ResultExecutingContext context) {
        if (context.Result is ObjectResult objectResult && objectResult.Value is IResult<object> actionResult) {
            if (actionResult.Success) {
                objectResult.Value = actionResult.Unwrap();
            } else {
                context.Result = CreateErrorObject(actionResult);
            }
        } else if (context.Result is IResult<IActionResult> contextResult) {
            context.Result = contextResult.Success ? contextResult.Unwrap() : CreateErrorObject(contextResult);
        }
    }

    ObjectResult CreateErrorObject(IResult<object> result) {
        ProblemDetails details;
        int statusCode;

        if (result.Errors.Count == 1) {
            var error = result.Errors.First();
            statusCode = error.StatusCode;

            details = new ProblemDetails {
                Title = error.Title,
                Detail = error.Message,
                Status = statusCode
            };
        } else {
            var errors = result.Errors.ToDictionary(e => e.Title, e => new[] { e.Message });
            statusCode = StatusCodes.Status400BadRequest;

            details = new ValidationProblemDetails(errors) {
                Title = "Error",
                Status = statusCode
            };
        }

        return new ObjectResult(details) { StatusCode = statusCode };
    }
}
