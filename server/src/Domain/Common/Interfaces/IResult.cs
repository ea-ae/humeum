using Domain.Common.Models;

namespace Domain.Common.Interfaces;


/// <summary>
/// Result object that contains either a result value or a collection of exceptions.
/// </summary>
/// <typeparam name="T">Type of result in case of success.</typeparam>
/// <typeparam name="E">List of exceptions in case of failure.</typeparam>
public interface IResult<out T, out E> where E : IBaseException {
    /// <summary>Whether the result is a success.</summary>
    bool Success { get; }
    /// <summary>Whether the result is a failure.</summary>
    bool Failure { get; }
    /// <summary>List of errors.</summary>
    /// <exception cref="InvalidOperationException">If the result was a success.</exception>
    IReadOnlyCollection<E> Errors { get; }
    /// <summary>Creates a result with the new value or the errors of the previous result.</summary>
    /// <typeparam name="TNew">Type of new result.</typeparam>
    /// <param name="value">Value of new result in case of success.</param>
    /// <returns>New result with value or the errors of the previous result.</returns>
    public IResult<TNew, E> Then<TNew>(TNew value);
    /// <summary>Unwraps the value.</summary>
    /// <exception cref="InvalidOperationException">If the result was a failure.</exception>
    T Unwrap();
}

/// <inheritdoc />
public interface IResult<out T> : IResult<T, IBaseException> { }
