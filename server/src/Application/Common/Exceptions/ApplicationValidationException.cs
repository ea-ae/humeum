using Domain.Common.Exceptions;

namespace Application.Common.Exceptions;

/// <summary>
/// This is a generic application validation exception, typically used for fields not matching certain requirements.
/// </summary>
public class ApplicationValidationException : ValidationException {
    public ApplicationValidationException(string message) : base(message) { }
}
