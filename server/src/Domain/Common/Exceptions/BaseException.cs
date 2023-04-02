using Domain.Common.Interfaces;

namespace Domain.Common.Exceptions;

/// <summary>
/// Base exception class that all domain and application exceptions derive from. The validation failure can be caused
/// by domain invariants or business rules.
/// </summary>
public abstract class BaseException : Exception, IBaseException {
    public BaseException(Exception inner) : base($"{inner.GetType().Name}: {inner.Message}", inner) { }

    public BaseException(string message) : base(message) { }

    public BaseException(string message, Exception inner) : base(message, inner) { }

    public abstract string Title { get;  }

    public abstract int StatusCode { get; }
}
