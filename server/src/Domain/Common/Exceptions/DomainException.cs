namespace Domain.Common.Exceptions;

/// <summary>
/// Domain exceptions are the validation exceptions of the domain layer, and signify violation of invariants.
/// </summary>
public class DomainException : Exception {
    public DomainException(Exception inner) : base($"{inner.GetType().Name}: {inner.Message}", inner) { }

    public DomainException(string message) : base(message) { }

    public DomainException(string message, Exception inner) : base(message, inner) { }
}
