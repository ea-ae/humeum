﻿using Shared.Interfaces;

using MediatR;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Web.Filters;
using Shared.Models;
using Application.V1.Profiles.Commands;
using Application.V1.Profiles.Queries;

namespace Web.Controllers;

/// <inheritdoc cref="Domain.V1.ProfileAggregate.Profile"/>
[Route("api/v{Version:ApiVersion}/users/{User}/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "CanHandleUserData")]
[ApplicationResultFilter]
[CsrfXHeaderFilter]
[Produces("application/json")]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
[ProducesResponseType(StatusCodes.Status403Forbidden)]
[ApiController]
public class ProfilesController : ControllerBase {
    private readonly IMediator _mediator;

    /// <summary>Initializes a new controller.</summary>
    public ProfilesController(IMediator mediator) => _mediator = mediator;

    /// <summary>
    /// Get profiles owned by a user.
    /// </summary>
    /// <response code="200">Returns the profiles.</response>
    /// <response code="401">If a user route is accessed without an authentication token.</response>
    /// <response code="403">If a user route is accessed with an invalid authentication token or CSRF header is missing.</response>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ProfileDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IResult<IEnumerable<ProfileDto>, IBaseException>>> GetProfiles(GetProfilesQuery query) {
        var profiles = await _mediator.Send(query);
        return Ok(profiles);
    }

    /// <summary>
    /// Returns profile with given ID owned by user.
    /// </summary>
    /// <response code="200">Returns the profile.</response>
    /// <response code="401">If a profile route is accessed without an authentication token.</response>
    /// <response code="403">If a profile route is accessed with an invalid authentication token or CSRF header is missing.</response>
    /// <response code="404">Profile with given ID was not found for user.</response>
    [HttpGet("{Profile}")]
    [ProducesResponseType(typeof(ProfileDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "CanHandleProfileData")]
    public async Task<ActionResult<IResult<ProfileDto, IBaseException>>> GetProfile(GetProfileQuery query) {
        var profile = await _mediator.Send(query);
        return Ok(profile);
    }

    /// <summary>
    /// Returns the projection chart for a profile.
    /// </summary>
    /// <response code="200">Returns the chart.</response>
    /// <response code="401">If a profile route is accessed without an authentication token.</response>
    /// <response code="403">If a profile route is accessed with an invalid authentication token or CSRF header is missing.</response>
    /// <response code="404">Profile with given ID was not found for user.</response>
    [HttpGet("{Profile}/chart")]
    [ProducesResponseType(typeof(ProjectionDto), StatusCodes.Status200OK)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "CanHandleProfileData")]
    public async Task<ActionResult<IResult<ProjectionDto, IBaseException>>> GenerateChart(GenerateChartQuery query) {
        var chart = await _mediator.Send(query);
        return Ok(chart);
    }

    /// <summary>
    /// Creates a new profile for an user. Users can have multiple profiles with their own
    /// configuration settings, assets, and transactions.
    /// </summary>
    /// <response code="201">Returns a location header to the newly created item.</response>
    /// <response code="400">If the field values did not satisfy domain invariants.</response>
    /// <response code="401">If a user route is accessed without an authentication token.</response>
    /// <response code="403">If a user route is accessed with an invalid authentication token or CSRF header is missing.</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IResult<IActionResult, IBaseException>> AddProfile(AddProfileCommand command) {
        var idResult = await _mediator.Send(command);
        return idResult.Then(id => Result<IActionResult, IBaseException>.Ok(
            CreatedAtAction(nameof(GetProfile), new { command.User, Profile = id }, null)));
    }

    /// <summary>
    /// Deletes a profile with the given ID.
    /// </summary>
    /// <response code="204">If profile was deleted.</response>
    /// <response code="401">If a user route is accessed without an authentication token.</response>
    /// <response code="403">If a user route is accessed with an invalid authentication token or CSRF header is missing.</response>
    /// <response code="404">If a profile with the given ID was not found.</response>
    [HttpDelete("{Profile}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IResult<IActionResult, IBaseException>> DeleteProfile(DeleteProfileCommand command) {
        var result = await _mediator.Send(command);
        return result.Then(NoContent());
    }
}
