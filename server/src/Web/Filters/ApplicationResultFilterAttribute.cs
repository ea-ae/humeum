﻿using Domain.Common.Interfaces;

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
        // todo: update IBaseException to contain a StatusCode field so we can get rid of this code smell
        if (context.Result is ObjectResult actionResult) {
            if (actionResult.Value is IResult<object> result) {
                if (result.Success) {
                    actionResult.Value = result.Unwrap();
                } else {
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
                    context.Result = new ObjectResult(details) { StatusCode = statusCode };
                }
            }
        }
    }
}
