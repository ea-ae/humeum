using Application.Common.Exceptions;
using Application.Common.Interfaces;

using Domain.UserAggregate;

using Infrastructure.Auth;
using Infrastructure.Persistence;

namespace Infrastructure.Services;

public abstract class ApplicationUserService : IApplicationUserService {
    protected readonly IAppDbContext _context;

    public ApplicationUserService(IAppDbContext context) => _context = context;

    public abstract Task<int> CreateUserAsync(User user, string password, bool rememberMe);

    public abstract Task<int> SignInUserAsync(string username, string password, bool rememberMe);

    protected abstract Task<string> CreateToken(ApplicationUser user);

    protected abstract void AddTokenAsCookie(string token);

    public async Task UpdateClientToken(int userId) {
        var appUser = GetApplicationUser(_context, userId);
        var token = await CreateToken(appUser);
        AddTokenAsCookie(token);
    }

    public User GetUserById(int userId) {
        var appUser = GetApplicationUser(_context, userId);

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

    protected ApplicationUser GetApplicationUser(IAppDbContext context, int userId) {
        if (_context is ApplicationDbContext identityContext) {
            var appUser = identityContext.Users.FirstOrDefault(au => au.Id == userId);

            if (appUser is null) {
                throw new NotFoundValidationException("User with provided ID could not be found.");
            }

            return appUser;
        }
        throw new InvalidOperationException("Invalid DbContext service provided.");
    }
}
