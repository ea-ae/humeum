using Domain.UserAggregate;

using Infrastructure.Auth;

namespace Infrastructure.Common.Extensions;

public static class ApplicationUserExtensions {
    public static User GetDomainUser(this ApplicationUser appUser) {
        string username = appUser.UserName ?? throw new InvalidOperationException();
        string email = appUser.Email ?? throw new InvalidOperationException();

        return new User(username, email);
    }
}
