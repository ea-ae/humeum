using Shared.Exceptions;

namespace Application.Common.Exceptions;

/// <summary>
/// This is a generic application validation exception, typically used for fields not matching certain requirements.
/// </summary>
public class ApplicationValidationException : BaseException {
    public ApplicationValidationException(string message) : base(message) { }

    public override string Title => "Validation Error";

    public override int StatusCode => 401;
}
