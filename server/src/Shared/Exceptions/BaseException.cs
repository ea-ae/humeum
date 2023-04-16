using Shared.Interfaces;

namespace Shared.Exceptions;

/// <inheritdoc cref="IBaseException "/>
public abstract class BaseException : Exception, IBaseException {
    /// <summary>Create an exception through an inner exception. The exception message is formatted to include the inner exception name.</summary>
    public BaseException(Exception inner) : base($"{inner.GetType().Name}: {inner.Message}", inner) { }

    /// <summary>Create an exception with a custom message.</summary>
    public BaseException(string message) : base(message) { }

    /// <summary>Create an exception with a custom message and inner exception.</summary>
    public BaseException(string message, Exception inner) : base(message, inner) { }

    /// <inheritdoc/>
    public abstract string Title { get; }

    /// <inheritdoc/>
    public abstract int StatusCode { get; }
}
