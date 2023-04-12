using Application.Common.Interfaces;

using Domain.UserAggregate;

namespace Application.Test.Common.Stubs;

internal class ApplicationUserServiceStub : IApplicationUserService {
    public Task<int> CreateUserAsync(User user, string password, bool rememberMe) {
        return Task.FromResult(1);
    }

    public User GetUserById(int id) {
        return new User(id, "test");
    }

    public Task<int> SignInUserAsync(string username, string password, bool rememberMe) {
        return Task.FromResult(1);
    }

    public Task UpdateClientToken(int userId) {
        return Task.CompletedTask;
    }

    public Task<int> RefreshUserAsync(int userId) {
        return Task.FromResult(1);
    }
}
