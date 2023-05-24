using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.V1.UserAggregate;
using Shared.Interfaces;
using Shared.Models;

namespace Application.Test.Common.Stubs;

internal class ApplicationUserServiceStub : IApplicationUserService {
    public Task<IResult<int, IAuthenticationException>> CreateUserAsync(User user, string password, bool rememberMe) {
        IResult<int, IAuthenticationException> result = Result<int, IAuthenticationException>.Ok(1);
        return Task.FromResult(result);
    }

    public Task<IResult<int, IAuthenticationException>> SignInUserAsync(string username, string password, bool rememberMe) {
        IResult<int, IAuthenticationException> result = Result<int, IAuthenticationException>.Ok(1);
        return Task.FromResult(result);
    }

    public Task<IResult<int, IAuthenticationException>> RefreshUserAsync(int userId) {
        IResult<int, IAuthenticationException> result = Result<int, IAuthenticationException>.Ok(1);
        return Task.FromResult(result);
    }

    public Task<IResult<None, NotFoundValidationException>> UpdateClientToken(int userId) {
        IResult<None, NotFoundValidationException> result = Result<None, NotFoundValidationException>.Ok(None.Value);
        return Task.FromResult(result);
    }

    public IResult<User, NotFoundValidationException> GetUserById(int id) {
        var user = new User(id, "test");
        return Result<User, NotFoundValidationException>.Ok(user);
    }

    public Task<IResult<None, IBaseException>> SignOutUserAsync() {
        throw new NotImplementedException();
    }

    public Task<IResult<int, IAuthenticationException>> RefreshUserAsync() {
        throw new NotImplementedException();
    }
}
