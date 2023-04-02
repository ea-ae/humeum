using Application.Common.Interfaces;

namespace Application.Common.Exceptions;

public class AuthenticationException : Exception, IAuthenticationException {
    public AuthenticationException(string message) : base(message) { }

    public string Title => "Authentication error";

    public int StatusCode => 401;
}
