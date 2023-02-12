using Application.Users.Commands.RegisterUser;
using Application.Users.Commands.SignInUser;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using Web.Filters;

namespace Web.Controllers;

[Route("api/v1/[controller]")]
[CsrfXHeaderFilter]
[ApiController]
public class UsersController : ControllerBase {
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator) => _mediator = mediator;

    [HttpGet("{id}")]
    public IActionResult Get() {
        return StatusCode(StatusCodes.Status503ServiceUnavailable);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterUserCommand command) {
        int id = await _mediator.Send(command);
        return CreatedAtAction(nameof(Get), new { id }, null);
    }
    
    [HttpPost("sign-in")]
    //[SetAsCookieFilter(CookieName = "Jwt")]
    public async Task<IActionResult> SignIn(SignInUserCommand command) {
        int id = await _mediator.Send(command);
        return Ok(id);
    }
}
