using Application.Users.Commands.RegisterUser;
using Application.Users.Commands.SignInUser;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using Web.Filters;

namespace Web.Controllers;

/// <inheritdoc cref="Domain.UserAggregate.User"/>
[Route("api/v1/[controller]")]
[ApplicationExceptionFilter]
[CsrfXHeaderFilter]
[Produces("application/json")]
[ApiController]
public class UsersController : ControllerBase {
    private readonly IMediator _mediator;

    /// <summary>Initializes a user controller.</summary>
    public UsersController(IMediator mediator) => _mediator = mediator;

    /// <summary>
    /// Get details of an user with given ID.
    /// </summary>
    [HttpGet("{user}")]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
    public IActionResult GetUser() {
        return StatusCode(StatusCodes.Status503ServiceUnavailable);
    }

    /// <summary>
    /// Register a new user with a username, email address, and password.
    /// </summary>
    /// <response code="201">Returns the location of newly created user and an authentication token.</response>
    /// <response code="400">If the user fields do not match domain or application rules.</response>
    /// <response code="401">If the user creation attempt fails, e.g. username or email is already in use.</response>
    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> RegisterUser(RegisterUserCommand command) {
        int id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetUser), new { User = id }, null);
    }

    /// <summary>
    /// Signs in as an user.
    /// </summary>
    /// <response code="200">Returns the ID of user and authentication token.</response>
    /// <response code="401">If the authentication attempt fails, e.g. incorrect password.</response>
    [HttpPost("sign-in")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> SignInUser(SignInUserCommand command) {
        int id = await _mediator.Send(command);
        return Ok(new { UserId = id });
    }
}
