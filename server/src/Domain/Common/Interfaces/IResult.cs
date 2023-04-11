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
    /// <summary>Creates a result with the new value or the errors of the previous result.</summary>
    /// <typeparam name="TNew">Type of new result.</typeparam>
    /// <param name="value">Value of new result in case of success.</param>
    /// <returns>New result with value or the errors of the previous result.</returns>
    public IResult<TNew, E> Then<TNew>(TNew value);
    /// <inheritdoc cref="Then{TNew}(TNew)"/>
    /// <typeparam name="ENew">Error type of new result.</typeparam>
    public IResult<TNew, ENew> Then<TNew, ENew>(Func<T, IResult<TNew, ENew>> then) where ENew : IBaseException;
    /// <inheritdoc cref="Then{TNew, ENew}(Func{T, IResult{TNew, ENew}})"/>
    public Task<IResult<TNew, ENew>> ThenAsync<TNew, ENew>(Func<T, Task<IResult<TNew, ENew>>> then) where ENew : IBaseException;
    /// <summary>Unwraps the value and returns it.</summary>
    /// <exception cref="InvalidOperationException">If the result was a failure.</exception>
    T Unwrap();
    /// <summary>List of errors.</summary>
    /// <exception cref="InvalidOperationException">If the result was a success.</exception>
    IReadOnlyCollection<E> GetErrors();
}

/// <inheritdoc />
public interface IResult<out T> : IResult<T, IBaseException> { }
