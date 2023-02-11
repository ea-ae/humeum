namespace Application.Common.Interfaces;

public interface IApplicationUserService {
    public Task<int> CreateUserAsync(string username, string email, string password, bool rememberMe);

    public Task<int> SignInUserAsync(string username, string password, bool rememberMe);
}
