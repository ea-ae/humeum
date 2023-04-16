using Application.Common.Interfaces;

using Shared.Exceptions;

namespace Application.Common.Exceptions;

public class AuthenticationException : BaseException, IAuthenticationException {
    public AuthenticationException(string message) : base(message) { }

    public override string Title => "Authentication error";

    public override int StatusCode => 401;
}
