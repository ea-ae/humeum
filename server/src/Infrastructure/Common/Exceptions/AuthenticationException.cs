namespace Application.Common.Exceptions;

public class AuthenticationException : Exception, IAuthenticationException {
    public AuthenticationException(string message) : base(message) { }
}
