using Application.Profiles.Commands.AddProfile;
using Application.Profiles.Commands.DeleteProfile;
using Application.Profiles.Queries;
using Application.Profiles.Queries.GetProfileDetails;
using Application.Profiles.Queries.GetProfilesQuery;

using MediatR;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Web.Filters;

namespace Web.Controllers;

/// <inheritdoc cref="Domain.ProfileAggregate.Profile"/>
[Route("api/v1/users/{user}/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "CanHandleUserData")]
[ApplicationExceptionFilter]
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
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ProfileDto>>> GetProfiles(GetProfilesQuery query) {
        var profiles = await _mediator.Send(query);
        return Ok(profiles);
    }

    /// <summary>
    /// Returns profile with given ID owned by user.
    /// </summary>
    /// <response code="200">Returns the profile.</response>
    /// <response code="401">If a user route is accessed without an authentication token.</response>
    /// <response code="403">If a user route is accessed with an invalid authentication token or CSRF header is missing.</response>
    /// <response code="404">Profile with given ID was not found for user.</response>
    [HttpGet("{profile}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProfileDto>> GetProfile(GetProfileQuery query) {
        var profile = await _mediator.Send(query);
        return Ok(profile);
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
    public async Task<IActionResult> AddProfile(AddProfileCommand command) {
        int id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetProfile), new { command.User, Profile = id }, null);
    }

    /// <summary>
    /// Deletes a profile with the given ID.
    /// </summary>
    /// <response code="204">If profile was deleted.</response>
    /// <response code="401">If a user route is accessed without an authentication token.</response>
    /// <response code="403">If a user route is accessed with an invalid authentication token or CSRF header is missing.</response>
    /// <response code="404">If a profile with the given ID was not found.</response>
    [HttpDelete("{profile}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteProfile(DeleteProfileCommand command) {
        await _mediator.Send(command);
        return NoContent();
    }
}
