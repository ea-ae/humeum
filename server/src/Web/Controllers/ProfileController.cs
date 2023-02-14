using Application.Profiles.Commands.AddProfile;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using Web.Filters;

namespace Web.Controllers;

[Route("api/v1/users/{user}/[controller]")]
[CsrfXHeaderFilter]
[ApiController]
public class ProfilesController : ControllerBase {
    private readonly IMediator _mediator;

    public ProfilesController(IMediator mediator) => _mediator = mediator;

    [HttpGet("{profile}")]
    public IActionResult Get() {
        return StatusCode(StatusCodes.Status503ServiceUnavailable);
    }

    [HttpPost]
    public async Task<IActionResult> Add(AddProfileCommand command) {
        int id = await _mediator.Send(command);
        int x = 3;
        int y = 4;
        return CreatedAtAction(nameof(Get), new { command.User, id }, null);
    }

    [HttpDelete("{profile}")]
    public async Task<IActionResult> Delete(DeleteProfileCommand command) {
        await _mediator.Send(command);
        return NoContent();
    }
}
