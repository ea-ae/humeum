using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

using Shared.Interfaces;

namespace Web.Filters;

/// <summary>
/// Handles result objects by either unwrapping them or returning HTTP error codes.
/// </summary>
/// <remarks>
/// If a controller action returns an <see cref="ObjectResult"/> containing an <see cref="IResult{T, E}"/> object, the result will be unwrapped
/// or transformed into an appropriate HTTP error (based on the <see cref="IBaseException.StatusCode"/> value).
/// 
/// The unwrapping process depends on the <see cref="IResult{T, E}.Unwrap"/> value. In case it is an <see cref="IActionResult"/>, the
/// <see cref="ObjectResult"/> will be replaced entirely with it. Otherwise, the <see cref="ObjectResult"/> itself (such as <see cref="OkResult"/>) will
/// persist, but its containing value will be unwrapped.
/// </remarks>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class ApplicationResultFilterAttribute : ResultFilterAttribute {
    /// <summary>
    /// Unwrap the result before execution or transform it to an HTTP error.
    /// </summary>
    /// <param name="context">Filter context.</param>
    public override void OnResultExecuting(ResultExecutingContext context) {
        if (context.Result is ObjectResult objectResult && objectResult.Value is IResult<object?, IBaseException> objectValueResult) {
            if (objectValueResult.Success && objectResult.Value is IResult<IActionResult, IBaseException> actionResult) {
                // when dealing with a result such as ObjectResult<Result<IActionResult<int>>>, replace it with IActionResult<int>
                context.Result = actionResult.Unwrap();
            } else if (objectValueResult.Success) {
                // when dealing with a result such as ObjectResult<Result<int>>, unwrap its response contents into ObjectResult<int>
                objectResult.Value = objectValueResult.Unwrap();
            } else {
                // when we are dealing with a failed result, replace the response contents with an error response
                context.Result = CreateErrorObject(objectValueResult);
            }
        } else if (context.Result is IResult<object?, IBaseException>) {
            throw new ArgumentException("Unexpected Result object returned from controller.");
        }

        // impossible path: context.Result is IResult<IActionResult, IBaseException> contextResult
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
