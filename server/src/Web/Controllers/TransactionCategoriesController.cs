using Application.TransactionCategories.Commands.AddCategory;
using Application.TransactionCategories.Commands.DeleteCategory;

using MediatR;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Web.Filters;

namespace Web.Controllers;

/// <inheritdoc cref="Domain.TransactionCategoryAggregate.TransactionCategory"/>
/// <response code="401">If a user route is accessed without an authentication token.</response>
/// <response code="403">If a user route is accessed with an authentication token assigned to another user ID.</response>
[Route("api/v1/users/{user}/profiles/{profile}/transactions/categories")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "CanHandleUserData")]
[ApplicationExceptionFilter]
[CsrfXHeaderFilter]
[Produces("application/json")]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
[ProducesResponseType(StatusCodes.Status403Forbidden)]
[ApiController]
public class TransactionCategoriesController : ControllerBase {
    private readonly IMediator _mediator;

    /// <summary>Initializes a new controller.</summary>
    public TransactionCategoriesController(IMediator mediator) => _mediator = mediator;

    /// <summary>
    /// Get details of a category with given ID.
    /// </summary>
    [HttpGet("{category}")]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
    public IActionResult GetCategory() {
        return StatusCode(StatusCodes.Status503ServiceUnavailable);
    }

    /// <summary>
    /// Create a new transaction category with a name.
    /// </summary>
    /// <response code="201">Returns a location header to the newly created item.</response>
    /// <response code="400">If the fields did not satisfy the domain invariants.</response>
    /// <response code="404">If a profile or transaction with the specified ID could not be found.</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> AddCategory(AddCategoryCommand command) {
        int id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetCategory), new { command.User, command.Profile, Category = id }, null);
    }

    /// <summary>
    /// Delete a category by its ID.
    /// </summary>
    /// <response code="204">If category is successfully deleted.</response>
    /// <response code="404">If a profile or profile-owned category with the specified ID could not be found.</response>
    [HttpDelete("{category}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteCategory(DeleteCategoryCommand command) {
        await _mediator.Send(command);
        return NoContent();
    }
}
