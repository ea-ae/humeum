using Application.Transactions.Commands;
using Application.Transactions.Queries;

using MediatR;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Web.Filters;

namespace Web.Controllers;

/// <inheritdoc cref="Domain.TransactionAggregate.Transaction"/>
[Route("api/v1/users/{user}/profiles/{profile}/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "CanHandleProfileData")]
[ApplicationExceptionFilter]
[CsrfXHeaderFilter]
[Produces("application/json")]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
[ProducesResponseType(StatusCodes.Status403Forbidden)]
[ApiController]
public class TransactionsController : ControllerBase {
    private readonly IMediator _mediator;

    /// <summary>Initializes a new controller.</summary>
    public TransactionsController(IMediator mediator) => _mediator = mediator;

    /// <summary>
    /// Get transactions for a specified user with optional filtering conditions.
    /// </summary>
    /// <response code="200">Returns the transactions.</response>
    /// <response code="401">If a user route is accessed without an authentication token.</response>
    /// <response code="403">If a user route is accessed with an invalid authentication token or CSRF header is missing.</response>
    /// <response code="404">If a profile with given ID wasn't found for the user.</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<TransactionDto>>> GetTransactions(GetTransactionsQuery query) {
        var transactions = await _mediator.Send(query);
        return Ok(transactions);
    }

    /// <summary>
    /// Get transaction with given ID.
    /// </summary>
    /// <response code="200">Returns the transaction.</response>
    /// <response code="401">If a user route is accessed without an authentication token.</response>
    /// <response code="403">If a user route is accessed with an invalid authentication token or CSRF header is missing.</response>
    /// <response code="404">If transaction or profile was not found.</response>
    [HttpGet("{transaction}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TransactionDto>> GetTransaction(GetTransactionQuery query) {
        var transaction = await _mediator.Send(query);
        return Ok(transaction);
    }

    /// <summary>
    /// Create a new transaction for a user profile. Transactions can either be single-payment, in which
    /// case the optional recurring transaction fields are not provided; or they can be recurrent,
    /// meaning they'll be performed at a certain frequency up until the payment end date.
    /// The first payment is always made at the payment start date. Positive amounts are income,
    /// negative amounts are expenses.
    /// </summary>
    /// <response code="201">Returns a location header to the newly created item.</response>
    /// <response code="400">If fields didn't satisfy domain invariants or the optional ones were only partially specified.</response>
    /// <response code="401">If a user route is accessed without an authentication token.</response>
    /// <response code="403">If a user route is accessed with an invalid authentication token or CSRF header is missing.</response>
    /// <response code="404">If a profile, tax scheme, or asset with a specified ID could not be found.</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> AddTransaction(AddTransactionCommand command) {
        int id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetTransaction), new { command.User, command.Profile, Transaction = id }, null);
    }

    /// <summary>
    /// Assign a category to transaction.
    /// </summary>
    /// <response code="201">If the category was successfully added.</response>
    /// <response code="401">If a user route is accessed without an authentication token.</response>
    /// <response code="403">If a user route is accessed with an invalid authentication token or CSRF header is missing.</response>
    /// <response code="404">If a profile or transaction with a corresponding ID could not be found.</response>
    /// <response code="409">If the category already exists on the transaction.</response>
    [HttpPost("{transaction}/categories")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> AddCategoryToTransaction(AddTransactionCategoryCommand command) {
        await _mediator.Send(command);
        return new ObjectResult(null) { StatusCode = StatusCodes.Status201Created };
    }

    ///// <summary>
    ///// Replaces a transaction's fields.
    ///// </summary>
    ///// <response code="204">If transaction was successfully replaced.</response>
    ///// <response code="400">If fields didn't satisfy domain invariants or the optional ones were only partially specified.</response>
    ///// <response code="401">If a user route is accessed without an authentication token.</response>
    ///// <response code="403">If a user route is accessed with an invalid authentication token or CSRF header is missing.</response>
    ///// <response code="404">If a profile, tax scheme, or asset with a specified ID could not be found.</response>
    //[HttpPut("{transaction}")]
    //[ProducesResponseType(StatusCodes.Status204NoContent)]
    //[ProducesResponseType(StatusCodes.Status400BadRequest)]
    //[ProducesResponseType(StatusCodes.Status404NotFound)]
    //public async Task<IActionResult> ReplaceTransaction(ReplaceTransactionCommand command) {
    //    await _mediator.Send(command);
    //    return NoContent();
    //}

    /// <summary>
    /// Deletes a transaction with given ID from a profile.
    /// </summary>
    /// <response code="204">If transaction was deleted.</response>
    /// <response code="401">If a user route is accessed without an authentication token.</response>
    /// <response code="403">If a user route is accessed with an invalid authentication token or CSRF header is missing.</response>
    /// <response code="404">If transaction or profile was not found.</response>
    [HttpDelete("{transaction}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteTransaction(DeleteTransactionCommand command) {
        await _mediator.Send(command);
        return NoContent();
    }

    /// <summary>
    /// Remove a category from a transaction.
    /// </summary>
    /// <response code="204">If the category was successfully removed.</response>
    /// <response code="401">If a user route is accessed without an authentication token.</response>
    /// <response code="403">If a user route is accessed with an invalid authentication token or CSRF header is missing.</response>
    /// <response code="404">If a profile, transaction, or category with corresponding ID was not found on the transaction.</response>
    [HttpDelete("{transaction}/categories")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> RemoveCategoryFromTransaction(RemoveTransactionCategoryCommand command) {
        await _mediator.Send(command);
        return NoContent();
    }
}
