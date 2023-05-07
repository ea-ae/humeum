using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using Application.Common.Exceptions;
using Application.Common.Extensions;
using Application.Common.Interfaces;

using AutoMapper;

using Domain.V1.UserAggregate;

using Infrastructure.Auth;
using Infrastructure.Common.Settings;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

using Shared.Interfaces;
using Shared.Models;

namespace Infrastructure.Services;

public class JwtApplicationUserService : ApplicationUserService {
    readonly UserManager<ApplicationUser> _userManager;
    readonly SignInManager<ApplicationUser> _signInManager;
    readonly IHttpContextAccessor _httpContextAccessor;
    readonly IMapper _mapper;
    readonly JwtSettings _jwtSettings;

    public JwtApplicationUserService(IAppDbContext context,
                                     UserManager<ApplicationUser> userManager,
                                     SignInManager<ApplicationUser> signInManager,
                                     IHttpContextAccessor httpContextAccessor,
                                     IMapper mapper,
                                     IOptions<JwtSettings> jwtSettings) : base(context) {
        _userManager = userManager;
        _signInManager = signInManager;
        _httpContextAccessor = httpContextAccessor;
        _mapper = mapper;
        _jwtSettings = jwtSettings.Value;
    }

    public override async Task<IResult<int, IAuthenticationException>> CreateUserAsync(User user, string password, bool rememberMe) {
        var appUser = _mapper.Map<ApplicationUser>(user);

        var result = await _userManager.CreateAsync(appUser, password);
        if (result.Succeeded) {
            await _signInManager.SignInAsync(appUser, isPersistent: rememberMe);
            appUser = await _userManager.FindByNameAsync(user.Username) ?? throw new InvalidOperationException();
            return (await SetupUserTokens(appUser)).Then(appUser.Id);
            //return appUser.Id;
        }

        var error = result.Errors.First();
        return Result<int, AuthenticationException>.Fail(new AuthenticationException($"{error.Code}\n{error.Description}"));
    }

    public override async Task<IResult<int, IAuthenticationException>> SignInUserAsync(string username, string password, bool rememberMe) {
        var result = await _signInManager.PasswordSignInAsync(username, password, isPersistent: rememberMe,
                                                              lockoutOnFailure: true);

        if (result.Succeeded) {
            var appUser = await _userManager.FindByNameAsync(username) ?? throw new InvalidOperationException();
            return (await SetupUserTokens(appUser)).Then(appUser.Id);
        }

        if (result.IsLockedOut) {
            return Result<int, AuthenticationException>.Fail(new AuthenticationException("Too many attempts, try again later."));
        }

        return Result<int, AuthenticationException>.Fail(new AuthenticationException("Sign-in attempt with username & password failed."));
    }

    public override async Task<IResult<int, IAuthenticationException>> RefreshUserAsync() {
        return await GetRefreshTokenFromCookie()
            .Then(refreshToken => _context.Users.ToFoundResult(u => u.RefreshToken == refreshToken).ThenError<IAuthenticationException>(new AuthenticationException("Invalid refresh token.")))
            .ThenAsync<int, IAuthenticationException>(async appUser => {
                if (appUser.RefreshTokenExpiry < DateTime.UtcNow) {
                    return Result<int, IAuthenticationException>.Fail(new AuthenticationException("Provided refresh token has expired."));
                }

                if (!appUser.Enabled) {
                    return Result<int, IAuthenticationException>.Fail(new AuthenticationException("User account has been disabled."));
                }

                // create new JWT token and log user in
                string token = await CreateToken(appUser);
                AddTokenAsCookie(token);
                await _signInManager.SignInAsync(appUser, isPersistent: true); // todo: sure about this?

                return Result<int, IAuthenticationException>.Ok(appUser.Id);
            });
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
            SameSite = SameSiteMode.Lax,
            Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.CookieExpireMinutes)
        };

        HttpContext httpContext = _httpContextAccessor.HttpContext ?? throw new InvalidOperationException();
        httpContext.Response.Cookies.Append(_jwtSettings.Cookie, token, cookieOptions);
    }

    protected void AddRefreshTokenAsCookie(string refreshToken) {
        var cookieOptions = new CookieOptions() {
            HttpOnly = true,
            SameSite = SameSiteMode.Lax,
            Expires = DateTime.UtcNow.AddDays(_jwtSettings.RefreshCookieExpireDays)
        };

        HttpContext httpContext = _httpContextAccessor.HttpContext ?? throw new InvalidOperationException();
        httpContext.Response.Cookies.Append(_jwtSettings.RefreshCookie, refreshToken, cookieOptions);
    }

    IResult<string, IAuthenticationException> GetRefreshTokenFromCookie() {
        HttpContext httpContext = _httpContextAccessor.HttpContext ?? throw new InvalidOperationException();
        bool found = httpContext.Request.Cookies.TryGetValue(_jwtSettings.RefreshCookie, out string? refreshToken);

        if (found) {
            return Result<string, IAuthenticationException>.Ok(refreshToken!);
        }
        return Result<string, IAuthenticationException>.Fail(new AuthenticationException("Refresh token cookie could not be found."));
    }

    async Task<IResult<None, IAuthenticationException>> SetupUserTokens(ApplicationUser user) {
        if (!user.Enabled) {
            return Result<None, AuthenticationException>.Fail(new AuthenticationException("User account has been disabled."));
        }

        string token = await CreateToken(user);
        AddTokenAsCookie(token);

        string refreshToken = CreateRefreshToken();
        AddRefreshTokenAsCookie(refreshToken);

        // persist refresh token in DB

        _context.Users.Update(user); // in case unattached
        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(_jwtSettings.RefreshCookieExpireDays);
        await _context.SaveChangesAsync();

        return Result<None, AuthenticationException>.Ok(None.Value);
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
