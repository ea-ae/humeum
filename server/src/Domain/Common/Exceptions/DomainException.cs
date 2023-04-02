namespace Domain.Common.Exceptions;

/// <summary>
/// Domain exceptions are the validation exceptions of the domain layer, and signify violation of invariants.
/// </summary>
public class DomainException : ValidationException {
    public DomainException(string message) : base(message) { }

    public DomainException(Exception inner) : base(inner) { }
}
