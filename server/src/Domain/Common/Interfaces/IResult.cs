namespace Domain.Common.Interfaces;

/// <summary>
/// Result object that contains either a result value or a collection of exceptions.
/// </summary>
/// <typeparam name="T">Type of result in case of success.</typeparam>
/// <typeparam name="E">List of exceptions in case of failure.</typeparam>
public interface IResult<T, E> where E : Exception {
    /// <summary>Whether the result is a success.</summary>
    bool Success { get; }
    /// <summary>Result value.</summary>
    /// <exception cref="InvalidOperationException">In case of a failure.</exception>
    T Value { get; }
    /// <summary>List of errors.</summary>
    /// <exception cref="InvalidOperationException">In case of a success.</exception>
    IReadOnlyCollection<E> Errors { get; }

    /// <summary>Try to unwrap the result value.</summary>
    /// <param name="value">Variable to store value in.</param>
    /// <param name="handleErrors">Action to handle a failure scenario.</param>
    void TryUnwrap(ref T value, Action<IReadOnlyCollection<E>>? handleErrors);
    /// <summary>Unwrap result value or provide a fallback.</summary>
    /// <param name="fallback">Function that provides a fallback.</param>
    /// <returns>Unwrapped value.</returns>
    T UnwrapOr(Func<IReadOnlyCollection<E>, T> fallback);
}

/// <inheritdoc />
public interface IResult<T> : IResult<T, Exception> { }
