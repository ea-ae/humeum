namespace Application.Common.Exceptions;

public class NotFoundValidationException : ValidationException {
    public NotFoundValidationException() { }

    public NotFoundValidationException(string message) : base(message) { }

    public NotFoundValidationException(string message, Exception inner) : base(message, inner) { }
}
