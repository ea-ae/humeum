using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using Application.Common.Exceptions;
using Application.Common.Interfaces;

using Domain.UserAggregate;

using Infrastructure.Auth;
using Infrastructure.Common.Settings;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Services;

public class JwtApplicationUserService : ApplicationUserService {
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly JwtSettings _jwtSettings;

    public JwtApplicationUserService(IAppDbContext context,
                                     UserManager<ApplicationUser> userManager,
                                     SignInManager<ApplicationUser> signInManager,
                                     IHttpContextAccessor httpContextAccessor,
                                     IOptions<JwtSettings> jwtSettings) : base(context) {
        _userManager = userManager;
        _signInManager = signInManager;
        _httpContextAccessor = httpContextAccessor;
        _jwtSettings = jwtSettings.Value;
    }

    public override async Task<int> CreateUserAsync(User user, string password, bool rememberMe) {
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
            await SetupUserTokens(appUser);
            return appUser.Id;
        }

        var error = result.Errors.First();
        throw new AuthenticationException($"{error.Code}\n{error.Description}");
    }

    public override async Task<int> SignInUserAsync(string username, string password, bool rememberMe) {
        var result = await _signInManager.PasswordSignInAsync(username, password, isPersistent: rememberMe,
                                                              lockoutOnFailure: true);

        if (result.Succeeded) {
            var user = await _userManager.FindByNameAsync(username) ?? throw new InvalidOperationException();
            await SetupUserTokens(user);
            return user.Id;
        }

        if (result.IsLockedOut) {
            throw new AuthenticationException("Too many attempts, try again later.");
        }

        throw new AuthenticationException("Sign-in attempt failed.");
    }

    public override async Task<int> RefreshUserAsync(int userId, string refreshToken) {
        var user = GetApplicationUser(userId);

        if (user.RefreshToken != refreshToken) {
            throw new AuthenticationException("Provided refresh token is invalid or does not match.");
        }

        if (user.RefreshTokenExpiry < DateTime.UtcNow) {
            throw new AuthenticationException("Provided refresh token has expired.");
        }

        await _signInManager.SignInAsync(user, isPersistent: true); // todo: sure about this?
        await SetupUserTokens(user);

        return user.Id;
    }

    protected override async Task<string> CreateToken(ApplicationUser user) {
        var key = Encoding.UTF8.GetBytes(_jwtSettings.Key);
        var secret = new SymmetricSecurityKey(key);
        var credentials = new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);

        var claims = await GetUserClaims(user);
        var expires = DateTime.UtcNow.AddDays(_jwtSettings.CookieExpireMinutes);

        var token = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Issuer,
            claims: claims,
            expires: expires,
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    protected override string CreateRefreshToken() => Guid.NewGuid().ToString();

    protected override void AddTokenAsCookie(string token) {
        var cookieOptions = new CookieOptions() {
            HttpOnly = true,
            Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.CookieExpireMinutes)
        };

        HttpContext httpContext = _httpContextAccessor.HttpContext ?? throw new InvalidOperationException();
        httpContext.Response.Cookies.Append(_jwtSettings.Cookie, token, cookieOptions);
    }

    protected void AddRefreshTokenAsCookie(string refreshToken) {
        var cookieOptions = new CookieOptions() {
            HttpOnly = true,
            Expires = DateTime.UtcNow.AddDays(_jwtSettings.RefreshCookieExpireDays)
        };

        HttpContext httpContext = _httpContextAccessor.HttpContext ?? throw new InvalidOperationException();
        httpContext.Response.Cookies.Append(_jwtSettings.RefreshCookie, refreshToken, cookieOptions);
    }

    async Task SetupUserTokens(ApplicationUser user) {
        string token = await CreateToken(user);
        AddTokenAsCookie(token);

        string refreshToken = CreateRefreshToken();
        AddRefreshTokenAsCookie(refreshToken);

        // persist tokens in DB

        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(_jwtSettings.RefreshCookieExpireDays);
        _context.Users.Update(user); // in case unattached
        await _context.SaveChangesAsync();
    }

    async Task<IEnumerable<Claim>> GetUserClaims(ApplicationUser user) {
        var claims = await _userManager.GetClaimsAsync(user);
        var profiles = _context.Profiles.Where(p => p.UserId == user.Id && p.DeletedAt == null).ToList();

        claims.Add(new Claim("uid", user.Id.ToString()));
        claims.Add(new Claim("name", user.DisplayName ?? throw new InvalidOperationException()));
        claims.Add(new Claim("profiles", string.Join(",", profiles.Select(p => p.Id))));

        return claims;
    }
}
