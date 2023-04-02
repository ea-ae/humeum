using Domain.Common.Interfaces;

namespace Domain.Common.Models;

/// <inheritdoc cref="IResult{T, E}" />
public class Result<T, E> : IResult<T, E> where E : IBaseException {
    public bool Success { get; private init; }

    readonly T? _value;
    public T Value => Success ? _value! : throw new InvalidOperationException("Result did not succeed, cannot access value.");

    readonly IReadOnlyCollection<E>? _errors;
    public IReadOnlyCollection<E> Errors => _errors ?? throw new InvalidOperationException("Result succeeded, cannot access errors.");

    public static Result<T, E> Ok(T value) {
        return new Result<T, E>(value);
    }

    public static Result<T, E> Fail(E error) {
        return new Result<T, E>(new List<E> { error });
    }

    public static Result<T, E> Fail(IReadOnlyCollection<E> errors) {
        return new Result<T, E>(errors);
    }

    //public void TryUnwrap(ref T value, Action<IReadOnlyCollection<E>>? handleErrors) {
    //    if (Success) {
    //        value = _value!;
    //    } else handleErrors?.Invoke(Errors);
    //}

    //public T UnwrapOr(Func<IReadOnlyCollection<E>, T> fallback) => Success ? Value : fallback.Invoke(Errors);

    protected Result(T value) {
        Success = true;
        _value = value;
    }

    protected Result(IReadOnlyCollection<E> errors) {
        Success = false;
        _errors = errors;
    }
}

public class Result<T> : Result<T, IBaseException>, IResult<T> {
    protected Result(T value) : base(value) { }

    protected Result(IReadOnlyCollection<IBaseException> errors) : base(errors) { }

    public static new Result<T> Ok(T value) {
        return new Result<T>(value);
    }

    public static new Result<T> Fail(IBaseException error) {
        return new Result<T>(new List<IBaseException> { error });
    }
}
