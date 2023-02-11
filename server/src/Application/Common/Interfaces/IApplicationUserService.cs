using Microsoft.Extensions.Options;

namespace Application.Common.Interfaces;

public interface IApplicationUserService {
    public Task<string> CreateUserAsync(string username, string email, string password, bool rememberMe);

    public Task<string> SignInUserAsync(string username, string password, bool rememberMe);
}
