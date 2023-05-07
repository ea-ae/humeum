namespace Shared.Interfaces;

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

    /// <summary>Unwraps the value and returns it.</summary>
    /// <exception cref="InvalidOperationException">If the result was a failure.</exception>
    T Unwrap();

    /// <summary>List of errors.</summary>
    /// <exception cref="InvalidOperationException">If the result was a success.</exception>
    IReadOnlyCollection<E> GetErrors();

    /// <summary>Transforms the result into another result unless it is an error, in case of which an unsuccessful result is returned.</summary>
    /// <typeparam name="TNew">Value type of the new result.</typeparam>
    /// <param name="value">New result.</param>
    /// <returns>Result with new value or the errors of the previous result.</returns>
    IResult<TNew, E> Then<TNew>(TNew value);

    /// <inheritdoc cref="Then{TNew}(TNew)"/>
    /// <typeparam name="TNew">Value type of the new result.</typeparam>
    /// <typeparam name="ENew">Error type of new result. Previous result errors must be castable to this type.</typeparam>
    IResult<TNew, ENew> Then<TNew, ENew>(Func<T, IResult<TNew, ENew>> then) where ENew : IBaseException;

    /// <inheritdoc cref="Then{TNew, ENew}(Func{T, IResult{TNew, ENew}})"/>
    Task<IResult<TNew, ENew>> ThenAsync<TNew, ENew>(Func<T, Task<IResult<TNew, ENew>>> then) where ENew : IBaseException;

    /// <summary>Transforms the result into a result with another error type, replacing the error in case it was a failure.</summary>
    /// <typeparam name="ENew">Error type of new result.</typeparam>
    /// <param name="error">Error value.</param>
    IResult<T, ENew> ThenError<ENew>(ENew error) where ENew : IBaseException;
}
