using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[Route("api/v1/[controller]")]
public class AdminController : ControllerBase {
    [HttpGet("authorization")]
    public string GetAuthorizationHeader() {
        var authorization = Request.Headers.Authorization.ToString();
        return authorization;
    }

    [HttpGet("is-authenticated")]
    public string GetAuthenticationStatus() {
        var isAuthed = User.Identity?.IsAuthenticated ?? false;
        return isAuthed.ToString();
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpGet("claims")]
    public string GetClaimCount() {
        var authorization = Request.Headers.Authorization.ToString();
        var userClaims = User.Claims.Count();
        var firstIdentityClaims = User.Identities.First().Claims.Count();
        var isAuthed = User.Identity?.IsAuthenticated;
        var authType = User.Identity?.AuthenticationType?.ToString();
        return $"Authorization: {authorization}\n" +
               $"User claim count: {userClaims}, {firstIdentityClaims}\n" +
               $"IsAuthenticated: {isAuthed}\nAuthentication type: {authType}";
    }
}
