using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.V1.UserAggregate;
using Infrastructure.Auth;
using Infrastructure.Persistence;

using Shared.Interfaces;
using Shared.Models;

namespace Infrastructure.Services;

public abstract class ApplicationUserService : IApplicationUserService {
    protected readonly AppDbContext _context;

    public ApplicationUserService(IAppDbContext context) {
        if (context is AppDbContext identityContext) {
            _context = identityContext;
        } else {
            throw new NotSupportedException("Unsupported DbContext service provided.");
        }
    }

    public abstract Task<IResult<int, IAuthenticationException>> CreateUserAsync(User user, string password, bool rememberMe);

    public abstract Task<IResult<int, IAuthenticationException>> SignInUserAsync(string username, string password, bool rememberMe);

    public abstract Task<IResult<None, IBaseException>> SignOutUserAsync();

    public abstract Task<IResult<int, IAuthenticationException>> RefreshUserAsync();

    protected abstract Task<string> CreateToken(ApplicationUser user);

    protected abstract string CreateRefreshToken();

    protected abstract void AddTokenAsCookie(string token);

    /// <summary>Create a new token for a user and assign it. This is useful to reflect new user data (e.g. profile creation/deletion).</summary>
    public async Task<IResult<None, NotFoundValidationException>> UpdateClientToken(int userId) {
        return await GetApplicationUser(userId).ThenAsync<None, NotFoundValidationException>(async appUser => {
            var token = await CreateToken(appUser);
            AddTokenAsCookie(token);
            return Result<None, NotFoundValidationException>.Ok(None.Value);
        });
    }

    public IResult<User, NotFoundValidationException> GetUserById(int userId) {
        return GetApplicationUser(userId).Then(appUser => {
            var profiles = _context.Profiles.Where(p => p.UserId == userId && p.DeletedAt == null).ToList();
            var user = new User(appUser.Id,
                                appUser.UserName ?? throw new InvalidOperationException("Username is unexpectedly missing."),
                                appUser.Email ?? throw new InvalidOperationException("Email is unexpectedly missing."),
                                profiles);

            return Result<User, NotFoundValidationException>.Ok(user);
        });
    }

    protected IResult<ApplicationUser, NotFoundValidationException> GetApplicationUser(int userId) {
        var appUser = _context.Users.FirstOrDefault(au => au.Id == userId);

        if (appUser is null) {
            var error = new NotFoundValidationException("User with provided ID could not be found.");
            return Result<ApplicationUser, NotFoundValidationException>.Fail(error);
        }

        return Result<ApplicationUser, NotFoundValidationException>.Ok(appUser);
    }
}
