using Application.Common.Interfaces;

using Domain.UserAggregate;

using Infrastructure.Persistence;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public abstract class ApplicationUserService : IApplicationUserService {
    protected readonly IAppDbContext _context;

    public ApplicationUserService(IAppDbContext context) => _context = context;

    public abstract Task<int> CreateUserAsync(User user, string password, bool rememberMe);

    public abstract Task<int> SignInUserAsync(string username, string password, bool rememberMe);

    public async Task<User> GetUserById(int id) {
        if (_context is ApplicationDbContext identityContext) {
            var appUser = await identityContext.Users.SingleAsync(au => au.Id == id);
            var user = new User(appUser.Id,
                                appUser.UserName ?? throw new InvalidOperationException("No username found"),
                                appUser.Email ?? throw new InvalidOperationException("No email found"));
            return user;
        }
        throw new InvalidOperationException("Invalid service combination provided.");
    }
}
