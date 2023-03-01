using Domain.UserAggregate;

namespace Application.Common.Interfaces;

public interface IApplicationUserService {
    public Task<int> CreateUserAsync(User user, string password, bool rememberMe);

    public Task<int> SignInUserAsync(string username, string password, bool rememberMe);

    public Task UpdateClientToken(int userId);

    public User GetUserById(int id);
}
