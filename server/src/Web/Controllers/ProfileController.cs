using Application.Profiles.Commands.AddProfile;
using Application.Profiles.Commands.DeleteProfile;
using Application.Profiles.Queries;
using Application.Profiles.Queries.GetUserProfileDetails;

using MediatR;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Web.Filters;

namespace Web.Controllers;

[Route("api/v1/users/{user}/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "CanHandleUserData")]
[ValidationExceptionFilter]
[CsrfXHeaderFilter]
[ApiController]
public class ProfilesController : ControllerBase {
    private readonly IMediator _mediator;

    public ProfilesController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    public async Task<IActionResult> GetAll(GetUserProfilesQuery query) {
        var profiles = await _mediator.Send(query);
        return Ok(profiles);
    }

    [HttpGet("{profile}")]
    public async Task<IActionResult> Get(GetUserProfileQuery query) {
        var profile = await _mediator.Send(query);
        return Ok(profile);
    }

    [HttpPost]
    public async Task<IActionResult> Create(AddProfileCommand command) {
        int id = await _mediator.Send(command);
        return CreatedAtAction(nameof(Get), new { command.User, Profile = id }, null);
    }

    [HttpDelete("{profile}")]
    public async Task<IActionResult> Delete(DeleteProfileCommand command) {
        await _mediator.Send(command);
        return NoContent();
    }
}
