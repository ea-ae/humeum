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

    [HttpPost("register")]
    public async Task<int> Register(RegisterUserCommand command) {
        return await _mediator.Send(command);
    }

    [HttpPost("sign-in")]
    //[SetAsCookieFilter(CookieName = "AuthToken")]
    public async Task<int> SignIn(SignInUserCommand command) {
        return await _mediator.Send(command);
    }
}
