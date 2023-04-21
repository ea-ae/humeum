using Application.Common.Exceptions;
using Domain.UserAggregate;

using Shared.Interfaces;

using Shared.Models;

namespace Application.Common.Interfaces;

public interface IApplicationUserService {
    public Task<IResult<int, IAuthenticationException>> CreateUserAsync(User user, string password, bool rememberMe);

    public Task<IResult<int, IAuthenticationException>> SignInUserAsync(string username, string password, bool rememberMe);

    public Task<IResult<int, IAuthenticationException>> RefreshUserAsync(int userId);

    public Task<IResult<None, NotFoundValidationException>> UpdateClientToken(int userId);

    public IResult<User, NotFoundValidationException> GetUserById(int id);
}
