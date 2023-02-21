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

[Route("api/v1/users/{user}/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "CanHandleUserData")]
[ValidationExceptionFilter]
[CsrfXHeaderFilter]
[ApiController]
public class ProfilesController : ControllerBase {
    private readonly IMediator _mediator;

    public ProfilesController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    public async Task<IActionResult> GetProfiles(GetProfilesQuery query) {
        var profiles = await _mediator.Send(query);
        return Ok(profiles);
    }

    [HttpGet("{profile}")]
    public async Task<IActionResult> GetProfile(GetProfileQuery query) {
        var profile = await _mediator.Send(query);
        return Ok(profile);
    }

    [HttpPost]
    public async Task<IActionResult> AddProfile(AddProfileCommand command) {
        int id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetProfile), new { command.User, Profile = id }, null);
    }

    [HttpDelete("{profile}")]
    public async Task<IActionResult> DeleteProfile(DeleteProfileCommand command) {
        await _mediator.Send(command);
        return NoContent();
    }
}
