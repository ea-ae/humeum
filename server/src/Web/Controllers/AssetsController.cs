using Application.Transactions.Commands.AddTransaction;
using Application.Transactions.Queries.GetTransaction;
using MediatR;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Web.Filters;
using Application.Assets.Queries;

namespace Web.Controllers;

/// <inheritdoc cref="Domain.AssetAggregate.Asset"/>
/// <response code="401">If a user route is accessed without an authentication token.</response>
/// <response code="403">If a user route is accessed with an authentication token assigned to another user ID.</response>
[Route("api/v1/users/{user}/profiles/{profile}/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "CanHandleUserData")]
[ApplicationExceptionFilter]
[CsrfXHeaderFilter]
[Produces("application/json")]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
[ProducesResponseType(StatusCodes.Status403Forbidden)]
[ApiController]
public class AssetsController : ControllerBase {
    private readonly IMediator _mediator;

    /// <summary>Initializes a new controller.</summary>
    public AssetsController(IMediator mediator) => _mediator = mediator;

    /// <summary>
    /// Get asset options for a specific user.
    /// </summary>
    /// <response code="200">Returns the assets.</response>
    /// <response code="404">If a profile with given ID wasn't found for the user.</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAssets(GetAssetsQuery query) {
        var assets = await _mediator.Send(query);
        return Ok(assets);
    }

    /// <summary>
    /// Get asset with given ID.
    /// </summary>
    /// <response code="200">Returns the asset.</response>
    /// <response code="404">If asset or profile was not found.</response>
    [HttpGet("{asset}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAsset(GetAssetQuery query) {
        var asset = await _mediator.Send(query);
        return Ok(asset);
    }

    /// <summary>
    /// Create a new asset for a profile that can be used for investment transactions.
    /// </summary>
    /// <response code="201">Returns a location header to the newly created item.</response>
    /// <response code="400">If the domain invariants or application validation rules weren't satisfied.</response>
    /// <response code="404">If a profile with the specified ID could not be found for the user.</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> AddAsset(AddTransactionCommand command) {
        int id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetAsset), new { command.User, command.Profile, Asset = id }, null);
    }
}
