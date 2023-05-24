using Application.V1.TransactionCategories.Commands;
using Application.V1.TransactionCategories.Queries;
using MediatR;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Web.Filters;

namespace Web.Controllers;

/// <inheritdoc cref="Domain.V1.TransactionCategoryAggregate.TransactionCategory"/>
[Route("api/v{Version:ApiVersion}/users/{User}/profiles/{Profile}/transactions/categories")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "CanHandleProfileData")]
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
    /// Get all categories that are either default or created by the profile.
    /// </summary>
    /// <response code="200">Returns all the transaction categories.</response>
    /// <response code="400">If the fields did not satisfy the domain invariants.</response>
    /// <response code="401">If a user route is accessed without an authentication token.</response>
    /// <response code="403">If a user route is accessed with an invalid authentication token or CSRF header is missing.</response>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<CategoryDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategories(GetCategoriesQuery query) {
        var categories = await _mediator.Send(query);
        return Ok(categories);
    }

    /// <summary>
    /// Get details of a category with given ID.
    /// </summary>
    /// <response code="200">Returns a transaction category with the specified ID.</response>
    /// <response code="400">If the fields did not satisfy the domain invariants.</response>
    /// <response code="401">If a user route is accessed without an authentication token.</response>
    /// <response code="403">If a user route is accessed with an invalid authentication token or CSRF header is missing.</response>
    /// <response code="404">If a transaction category with given ID was not found.</response>
    [HttpGet("{Category}")]
    [ProducesResponseType(typeof(CategoryDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CategoryDto>> GetCategory(GetCategoryQuery query) {
        var category = await _mediator.Send(query);
        return Ok(category);
    }

    /// <summary>
    /// Create a new transaction category with a name.
    /// </summary>
    /// <response code="201">Returns a location header to the newly created item.</response>
    /// <response code="400">If the fields did not satisfy the domain invariants.</response>
    /// <response code="401">If a user route is accessed without an authentication token.</response>
    /// <response code="403">If a user route is accessed with an invalid authentication token or CSRF header is missing.</response>
    /// <response code="404">If a profile or transaction with the specified ID could not be found.</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> AddCategory(int user, AddCategoryCommand command) {
        int id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetCategory), new { user, command.Profile, Category = id }, null);
    }

    /// <summary>
    /// Delete a category by its ID.
    /// </summary>
    /// <response code="204">If category is successfully deleted.</response>
    /// <response code="401">If a user route is accessed without an authentication token.</response>
    /// <response code="403">If a user route is accessed with an invalid authentication token or CSRF header is missing.</response>
    /// <response code="404">If a profile or profile-owned category with the specified ID could not be found.</response>
    [HttpDelete("{Category}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteCategory(DeleteCategoryCommand command) {
        await _mediator.Send(command);
        return NoContent();
    }
}
