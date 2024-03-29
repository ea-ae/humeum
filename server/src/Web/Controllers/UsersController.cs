﻿using Application.Users.Commands;
using Application.Users.Queries;

using MediatR;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Shared.Interfaces;
using Shared.Models;

using Web.Filters;

namespace Web.Controllers;

/// <inheritdoc cref="Domain.V1.UserAggregate.User"/>
[Route("api/v{Version:ApiVersion}/[controller]")]
[ApplicationResultFilter]
[CsrfXHeaderFilter]
[Produces("application/json")]
[ApiController]
public class UsersController : ControllerBase {
    private readonly IMediator _mediator;

    /// <summary>Initializes a user controller.</summary>
    public UsersController(IMediator mediator) => _mediator = mediator;

    /// <summary>
    /// Get details of a user with a given ID.
    /// </summary>
    /// <response code="200">Details of user with given ID.</response>
    /// <response code="401">If a user route is accessed without an authentication token.</response>
    /// <response code="403">If a user route is accessed with an authentication token assigned to another user ID.</response>
    /// <response code="404">If a user with the specified ID could not be found.</response>
    [HttpGet("{User:int}")]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "CanHandleUserData")]
    public async Task<ActionResult<IResult<UserDto, IBaseException>>> GetUser(GetUserQuery query) {
        var user = await _mediator.Send(query);
        return Ok(user);
    }

    /// <summary>
    /// Get details of a user with a given ID.
    /// </summary>
    /// <response code="200">Details of user with given ID.</response>
    /// <response code="401">If a user route is accessed without an authentication token.</response>
    /// <response code="403">If a user route is accessed with an authentication token assigned to another user ID.</response>
    /// <response code="404">If a user with the specified ID could not be found.</response>
    [HttpGet("me")]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<ActionResult<IResult<UserDto, IBaseException>>> GetCurrentUser() {
        var currentUserId = User.Claims.First(claim => claim.Type == "uid");
        var query = new GetUserQuery { User = int.Parse(currentUserId.Value) };
        var user = await _mediator.Send(query);
        return Ok(user);
    }

    /// <summary>
    /// Register a new user with a username, email address, and password.
    /// </summary>
    /// <response code="201">Returns the location of newly created user and an authentication token.</response>
    /// <response code="400">If the user fields do not match domain or application rules.</response>
    /// <response code="401">If the user creation attempt fails, e.g. username or email is already in use.</response>
    [HttpPost("register")]
    [ProducesResponseType(typeof(CreatedAtActionResult), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IResult<IActionResult, IBaseException>> RegisterUser(RegisterUserCommand command) {
        var userIdResult = await _mediator.Send(command);
        return userIdResult.Then(userId => Result<IActionResult, IBaseException>.Ok(
            CreatedAtAction(nameof(GetUser), new { User = userId }, null)));
    }

    /// <summary>
    /// Signs in as a user.
    /// </summary>
    /// <response code="200">Details of signed in user.</response>
    /// <response code="401">If the authentication attempt fails, e.g. incorrect password.</response>
    [HttpPost("sign-in")]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<IResult<UserDto, IBaseException>>> SignInUser(SignInUserCommand command) {
        var userIdResult = await _mediator.Send(command);
        var user = await userIdResult.ThenAsync(async userId => await _mediator.Send(new GetUserQuery { User = userId }));
        return Ok(user);
    }

    /// <summary>
    /// Signs out the user.
    /// </summary>
    /// <response code="204">If the user is logged out successfully.</response>
    [HttpPost("sign-out")]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IResult<IActionResult, IBaseException>> SignOutUser(SignOutUserCommand command) {
        var result = await _mediator.Send(command);
        return result.Then(NoContent());
    }

    /// <summary>
    /// Refreshes user authentication through a refresh token.
    /// </summary>
    /// <response code="200">Details of signed in user.</response>
    /// <response code="401">If the authentication attempt fails, e.g. invalid refresh token.</response>
    [HttpPost("refresh")]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    public async Task<ActionResult<IResult<UserDto, IBaseException>>> RefreshUser(RefreshUserCommand command) {
        var userIdResult = await _mediator.Send(command);
        var user = await userIdResult.ThenAsync(async userId => await _mediator.Send(new GetUserQuery { User = userId }));
        return Ok(user);
    }
}
