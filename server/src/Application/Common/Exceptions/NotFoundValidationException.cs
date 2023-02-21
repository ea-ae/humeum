namespace Application.Common.Exceptions;

public class NotFoundValidationException : ValidationException {
    public NotFoundValidationException(string message) : base(message) { }

    public NotFoundValidationException(Type missingValueType) : base($"Requested {missingValueType.Name.ToLower()} does not exist.") { }

    public NotFoundValidationException(string message, Type missingValueType) : base($"{message}: {missingValueType.Name}.") { }
}
