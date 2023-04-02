namespace Domain.Common.Exceptions;

/// <summary>
/// Base exception class that all domain and application exceptions derive from. The validation failure can be caused
/// by domain invariants or business rules.
/// </summary>
public abstract class ValidationException : Exception {
    public ValidationException(Exception inner) : base($"{inner.GetType().Name}: {inner.Message}", inner) { }

    public ValidationException(string message) : base(message) { }

    public ValidationException(string message, Exception inner) : base(message, inner) { }
}
