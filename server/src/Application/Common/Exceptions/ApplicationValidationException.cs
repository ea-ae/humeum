namespace Application.Common.Exceptions;

/// <summary>
/// This is a generic application validation exception, typically used for fields not matching certain requirements.
/// </summary>
public class ApplicationValidationException : Exception {
    public ApplicationValidationException(Exception inner) : base($"{inner.GetType().Name}: {inner.Message}", inner) { }

    public ApplicationValidationException(string message) : base(message) { }

    public ApplicationValidationException(string message, Exception inner) : base(message, inner) { }
}
