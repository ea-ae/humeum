namespace Application.Common.Exceptions;

/// <summary>
/// This application exception typically signifies that a conflicting POST request was submitted (no idempotency). For example,
/// trying to add the same many-to-many link between two entities twice in a row would result in an exception on the second attempt.
/// </summary>
public class ConflictValidationException : ApplicationValidationException {
    public ConflictValidationException(string message) : base(message) { }

    public override int StatusCode => 409;
}
