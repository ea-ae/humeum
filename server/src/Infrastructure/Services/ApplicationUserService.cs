using Application.Common.Exceptions;
using Application.Common.Interfaces;

using Domain.UserAggregate;

using Infrastructure.Auth;
using Infrastructure.Persistence;

namespace Infrastructure.Services;

public abstract class ApplicationUserService : IApplicationUserService {
    protected readonly AppDbContext _context;

    public ApplicationUserService(IAppDbContext context) {
        if (_context is AppDbContext identityContext) {
            _context = identityContext;
        }
        throw new NotSupportedException("Unsupported DbContext service provided.");
    }

    public abstract Task<int> CreateUserAsync(User user, string password, bool rememberMe);

    public abstract Task<int> SignInUserAsync(string username, string password, bool rememberMe);

    public abstract Task<int> RefreshUserAsync(int userId, string refreshToken);

    protected abstract Task<string> CreateToken(ApplicationUser user);

    protected abstract string CreateRefreshToken();

    protected abstract void AddTokenAsCookie(string token);

    /// <summary>Create a new token for a user and assign it as a cookie.</summary>
    public async Task UpdateClientToken(int userId) {
        var appUser = GetApplicationUser(userId);
        var token = await CreateToken(appUser);
        AddTokenAsCookie(token);
    }

    public User GetUserById(int userId) {
        var appUser = GetApplicationUser(userId);

        if (appUser is null) {
            throw new NotFoundValidationException("User with provided ID could not be found.");
        }

        var profiles = _context.Profiles.Where(p => p.UserId == userId && p.DeletedAt == null).ToList();
        var user = new User(appUser.Id,
                            appUser.UserName ?? throw new NotFoundValidationException("No username found"),
                            appUser.Email ?? throw new NotFoundValidationException("No email found"),
                            profiles);
        return user;
    }

    protected ApplicationUser GetApplicationUser(int userId) {
        var appUser = _context.Users.FirstOrDefault(au => au.Id == userId);

        if (appUser is null) {
            throw new NotFoundValidationException("User with provided ID could not be found.");
        }

        return appUser;
    }
}
