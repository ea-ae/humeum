using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[Route("api/v1/[controller]")]
public class AdminController : ControllerBase {
    [HttpGet("claims")]
    public string GetClaims() {
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
