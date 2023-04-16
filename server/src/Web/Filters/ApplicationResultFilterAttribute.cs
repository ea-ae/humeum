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
    /// Unwrap the result before execution or transform it to an HTTP error. In case the result itself contains an <see cref="IActionResult"/>,
    /// unwrap the result and replace the response entirely.
    /// </summary>
    /// <param name="context">Filter context.</param>
    public override void OnResultExecuting(ResultExecutingContext context) {
        if (context.Result is ObjectResult objectResult && objectResult.Value is IResult<object?, IBaseException> objectValueResult) {
            if (objectValueResult.Success && objectResult.Value is IResult<IActionResult, IBaseException> actionResult) {
                // when a result contains an IActionResult, e.g.: result.Then(NoContent()), replace the response
                context.Result = actionResult.Unwrap(); 
            }
            
            if (objectValueResult.Success) {
                // when we are dealing with a result such as an Ok(Result<T>), unwrap its response contents
                objectResult.Value = objectValueResult.Unwrap(); 
            } else {
                // when we are dealing with a failed result, replace the response contents with an error response
                context.Result = CreateErrorObject(objectValueResult);
            }
        } else if (context.Result is IResult<IActionResult, IBaseException> contextResult) {
            context.Result = contextResult.Success ? contextResult.Unwrap() : CreateErrorObject(contextResult);
        } else if (context.Result is IResult<object?, IBaseException>) {
            throw new ArgumentException("Unexpected Result object with a non-IActionResult value returned from controller.");
        }
    }

    static ObjectResult CreateErrorObject(IResult<object?, IBaseException> result) {
        ProblemDetails details;
        int statusCode;

        var errors = result.GetErrors();
        if (errors.Count == 1) {
            var error = errors.First();
            statusCode = error.StatusCode;

            details = new ProblemDetails {
                Title = error.Title,
                Detail = error.Message,
                Status = statusCode
            };
        } else {
            var errorsDictionary = errors.GroupBy(e => e.Title).ToDictionary(g => g.Key, g => g.Select(e => e.Message).ToArray());
            statusCode = StatusCodes.Status400BadRequest;

            details = new ValidationProblemDetails(errorsDictionary) {
                Title = "Error",
                Status = statusCode
            };
        }

        return new ObjectResult(details) { StatusCode = statusCode };
    }
}
