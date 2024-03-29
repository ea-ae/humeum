﻿using Application.V1.Assets.Commands;
using Application.V1.Assets.Queries;

using Domain.V1.AssetAggregate.ValueObjects;

using MediatR;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Shared.Interfaces;
using Shared.Models;

using Web.Filters;

namespace Web.Controllers;

/// <inheritdoc cref="Domain.V1.AssetAggregate.Asset"/>
[Route("api/v{Version:ApiVersion}/users/{User}/profiles/{Profile}/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "CanHandleProfileData")]
[ApplicationResultFilter]
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
    /// <response code="401">If a user route is accessed without an authentication token.</response>
    /// <response code="403">If a user route is accessed with an invalid authentication token or CSRF header is missing.</response>
    /// <response code="404">If a profile with given ID wasn't found for the user.</response>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<AssetDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IResult<IEnumerable<AssetDto>, IBaseException>>> GetAssets(GetAssetsQuery query) {
        var assets = await _mediator.Send(query);
        return Ok(assets);
    }

    /// <summary>
    /// Get asset with given ID.
    /// </summary>
    /// <response code="200">Returns the asset.</response>
    /// <response code="404">If asset or profile was not found.</response>
    /// <response code="401">If a user route is accessed without an authentication token.</response>
    /// <response code="403">If a user route is accessed with an invalid authentication token or CSRF header is missing.</response>
    [HttpGet("{Asset}")]
    [ProducesResponseType(typeof(AssetDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IResult<AssetDto, IBaseException>>> GetAsset(GetAssetQuery query) {
        var asset = await _mediator.Send(query);
        return Ok(asset);
    }

    /// <summary>
    /// Get existing asset types.
    /// </summary>
    /// <response code="200">Returns the asset types.</response>
    /// <response code="401">If a user route is accessed without an authentication token.</response>
    /// <response code="403">If a user route is accessed with an invalid authentication token or CSRF header is missing.</response>
    /// <response code="404">If a profile with given ID wasn't found for the user.</response>
    [HttpGet("types")]
    [ProducesResponseType(typeof(IEnumerable<AssetType>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IResult<IEnumerable<AssetTypeDto>, IBaseException>>> GetAssetTypes(GetAssetTypesQuery query) {
        var assetTypes = await _mediator.Send(query);
        return Ok(assetTypes);
    }

    /// <summary>
    /// Create a new asset for a profile that can be used for investment transactions.
    /// </summary>
    /// <response code="201">Returns a location header to the newly created item.</response>
    /// <response code="400">If the domain invariants or application validation rules weren't satisfied.</response>
    /// <response code="401">If a user route is accessed without an authentication token.</response>
    /// <response code="403">If a user route is accessed with an invalid authentication token or CSRF header is missing.</response>
    /// <response code="404">If a profile or asset type with the specified ID could not be found for the user.</response>
    [HttpPost]
    [ProducesResponseType(typeof(IActionResult), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IResult<IActionResult, IBaseException>> AddAsset(AddAssetCommand command) {
        var result = await _mediator.Send(command);
        return result.Then(asset => Result<IActionResult, IBaseException>.Ok(
            CreatedAtAction(nameof(GetAsset), new { command.User, command.Profile, asset }, null)));
    }

    /// <summary>
    /// Replaces an asset's fields.
    /// </summary>
    /// <response code="204">If asset was successfully replaced.</response>
    /// <response code="400">If fields didn't satisfy domain invariants or the optional ones were only partially specified.</response>
    /// <response code="401">If a user route is accessed without an authentication token.</response>
    /// <response code="403">If a user route is accessed with an invalid authentication token or CSRF header is missing.</response>
    /// <response code="404">If a profile or asset type with the specified ID could not be found.</response>
    [HttpPut("{Asset}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IResult<IActionResult, IBaseException>> ReplaceAsset(ReplaceAssetCommand command) {
        var result = await _mediator.Send(command);
        return result.Then(NoContent());
    }

    /// <summary>
    /// Deletes a custom asset with given ID from a profile.
    /// </summary>
    /// <response code="204">If asset was deleted.</response>
    /// <response code="401">If a user route is accessed without an authentication token.</response>
    /// <response code="403">If a user route is accessed with an invalid authentication token or CSRF header is missing.</response>
    /// <response code="404">If asset or profile was not found.</response>
    [HttpDelete("{Asset}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IResult<IActionResult, IBaseException>> DeleteAsset(DeleteAssetCommand command) {
        var result = await _mediator.Send(command);
        return result.Then(NoContent());
    }
}
