using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using Application.Common.Exceptions;
using Application.Common.Interfaces;

using Domain.UserAggregate;

using Infrastructure.Common.Settings;
using Infrastructure.Identity;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Services;

public class JwtApplicationUserService : IApplicationUserService {
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly JwtSettings _jwtSettings;

    public JwtApplicationUserService(UserManager<ApplicationUser> userManager,
                                     SignInManager<ApplicationUser> signInManager,
                                     IHttpContextAccessor httpContextAccessor,
                                     IOptions<JwtSettings> jwtSettings) {
        _userManager = userManager;
        _signInManager = signInManager;
        _httpContextAccessor = httpContextAccessor;
        _jwtSettings = jwtSettings.Value;
    }

    public async Task<int> CreateUserAsync(User user, string password, bool rememberMe) {
        // todo automapper
        var appUser = new ApplicationUser {
            DisplayName = user.Username,
            UserName = user.Username,
            Email = user.Email,
        };

        var result = await _userManager.CreateAsync(appUser, password);
        if (result.Succeeded) {
            await _signInManager.SignInAsync(appUser, isPersistent: rememberMe);
            appUser = await _userManager.FindByNameAsync(user.Username) ?? throw new InvalidOperationException();
            var token = await CreateToken(appUser);
            AddTokenAsCookie(token);

            return user.Id;
        }

        var error = result.Errors.First();
        throw new AuthenticationException($"{error.Code}\n{error.Description}");
    }

    public async Task<int> SignInUserAsync(string username, string password, bool rememberMe) {
        var result = await _signInManager.PasswordSignInAsync(username, password, isPersistent: rememberMe,
                                                              lockoutOnFailure: true);

        if (result.Succeeded) {
            var user = await _userManager.FindByNameAsync(username) ?? throw new InvalidOperationException();
            var token = await CreateToken(user);
            AddTokenAsCookie(token);

            return user.Id;
        }

        if (result.IsLockedOut) {
            throw new AuthenticationException("Too many attempts, try again later.");
        }

        throw new AuthenticationException("Sign-in attempt failed.");
    }

    async Task<string> CreateToken(ApplicationUser user) {
        var key = Encoding.UTF8.GetBytes(_jwtSettings.Key);
        var secret = new SymmetricSecurityKey(key);
        var credentials = new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);

        var claims = await GetUserClaims(user);
        var expires = DateTime.UtcNow.AddDays(_jwtSettings.ExpireDays);

        var token = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Issuer,
            claims: claims,
            expires: expires,
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    async Task<IEnumerable<Claim>> GetUserClaims(ApplicationUser user) {
        var claims = await _userManager.GetClaimsAsync(user);

        claims.Add(new Claim("uid", user.Id.ToString()));
        claims.Add(new Claim("name", user.UserName ?? throw new InvalidOperationException()));

        return claims;
    }

    void AddTokenAsCookie(string token) {
        var cookieName = _jwtSettings.Cookie;
        var cookieOptions = new CookieOptions() {
            HttpOnly = true,
            Expires = DateTime.UtcNow.AddDays(_jwtSettings.ExpireDays)
        };

        HttpContext httpContext = _httpContextAccessor.HttpContext ?? throw new InvalidOperationException();
        httpContext.Response.Cookies.Append(cookieName, token, cookieOptions);
    }
}
