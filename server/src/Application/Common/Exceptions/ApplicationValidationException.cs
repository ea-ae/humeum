namespace Application.Common.Exceptions;

public class ApplicationValidationException : Exception {
    public ApplicationValidationException(Exception inner) : base($"{inner.GetType().Name}: {inner.Message}", inner) { }

    public ApplicationValidationException(string message) : base(message) { }

    public ApplicationValidationException(string message, Exception inner) : base(message, inner) { }
}
