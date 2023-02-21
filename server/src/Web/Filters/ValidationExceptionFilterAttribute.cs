using Application.Common.Exceptions;

using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Web.Filters;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class ValidationExceptionFilterAttribute : ExceptionFilterAttribute {
    public override void OnException(ExceptionContext context) {
        if (context.Exception is NotFoundValidationException notFoundValidationException) {
            var error = new ProblemDetails {
                Title = "Not found",
                Detail = context.Exception.Message,
                Status = StatusCodes.Status404NotFound
            };

            context.Result = new ObjectResult(error) {
                StatusCode = StatusCodes.Status404NotFound
            };
            context.ExceptionHandled = true;
        } else if (context.Exception is ValidationException validationException) {
            var error = new ProblemDetails {
                Title = "Validation error",
                Detail = context.Exception.Message,
                Status = StatusCodes.Status400BadRequest
            };

            context.Result = new ObjectResult(error) {
                StatusCode = StatusCodes.Status400BadRequest
            };
            context.ExceptionHandled = true;
        }
    }
}
