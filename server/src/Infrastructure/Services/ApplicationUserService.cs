using Application.Common.Exceptions;
using Application.Common.Interfaces;

using Domain.UserAggregate;

using Infrastructure.Persistence;

namespace Infrastructure.Services;

public abstract class ApplicationUserService : IApplicationUserService {
    protected readonly IAppDbContext _context;

    public ApplicationUserService(IAppDbContext context) => _context = context;

    public abstract Task<int> CreateUserAsync(User user, string password, bool rememberMe);

    public abstract Task<int> SignInUserAsync(string username, string password, bool rememberMe);

    public User GetUserById(int id) {
        if (_context is ApplicationDbContext identityContext) {
            var appUser = identityContext.Users.FirstOrDefault(au => au.Id == id);

            if (appUser is null) {
                throw new NotFoundValidationException("User with provided ID could not be found.");
            }

            var profiles = identityContext.Profiles.Where(p => p.UserId == id && p.DeletedAt == null).ToList();
            var user = new User(appUser.Id,
                                appUser.UserName ?? throw new NotFoundValidationException("No username found"),
                                appUser.Email ?? throw new NotFoundValidationException("No email found"),
                                profiles);
            return user;
        }
        throw new InvalidOperationException("Invalid DbContext service provided.");
    }
}
