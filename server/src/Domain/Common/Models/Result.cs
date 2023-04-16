using Domain.Common.Interfaces;

namespace Domain.Common.Models;

/// <inheritdoc cref="IResult{T, E}" />
public class Result<T, E> : IResult<T, E> where E : IBaseException {
    public class Builder {
        bool _hasValue = false;
        readonly List<E> _errors = new();

        T _value = default!;
        public T Value { get => _hasValue ? _value : throw new InvalidOperationException("Value is not set."); }

        public bool HasErrors => _errors.Any();

        public Builder() { }

        /// <summary>
        /// Adds a value to the result builder.
        /// </summary>
        /// <param name="value">Value to add.</param>
        /// <returns>Fluent interface self reference.</returns>
        /// <exception cref="InvalidOperationException">If a value or errors have been already added.</exception>
        public Builder AddValue(T value) {
            if (_hasValue) {
                throw new InvalidOperationException("Result builder already has a value.");
            }
            _value = value;
            _hasValue = true;
            return this;
        }

        /// <summary>
        /// Adds an error to the result builder.
        /// </summary>
        /// <param name="error">Error to add.</param>
        /// <returns>Fluent interface self reference.</returns>
        public Builder AddError(E error) {
            _errors.Add(error);
            return this;
        }

        /// <summary>
        /// Add the value or errors of a result, depending on whether it succeeded.
        /// </summary>
        /// <typeparam name="TAny">Value type of the result whose value or errors will be added.</typeparam>
        /// <param name="result">Result whose value or errors will be added.</param>
        /// <returns>Fluent interface self reference.</returns>
        public Builder AddResultValueOrErrors(IResult<T, E> result) {
            if (result.Success) {
                AddValue(result.Unwrap());
            } else {
                _errors.AddRange(result.GetErrors());
            }
            return this;
        }

        /// <summary>
        /// Add the errors of a result in case it was a failure.
        /// </summary>
        /// <typeparam name="TAny">Value type of the result whose errors will be added.</typeparam>
        /// <param name="result">Result whose errors will be added.</param>
        /// <returns>Fluent interface self reference.</returns>
        public Builder AddResultErrors<TAny>(IResult<TAny, E> result) {
            if (result.Failure) {
                _errors.AddRange(result.GetErrors());
            }
            return this;
        }

        /// <summary>
        /// Call a method on a value that returns a result and either assign its value to the builder value or add its errors.
        /// </summary>
        /// <param name="result">Result to set new value or add errors from in case there are any.</param>
        /// <returns>Fluent interface self reference.</returns>
        public Builder Transform(Func<T, IResult<T, E>> transformValue) {
            if (!_hasValue) {
                throw new InvalidOperationException("Value is not set at time of transform.");
            }

            var result = transformValue.Invoke(_value);
            if (result.Success) {
                _value = result.Unwrap();
            } else {
                _errors.AddRange(result.GetErrors());
            }

            return this;
        }

        /// <summary>
        /// Call a method on the value and add any errors from its returned Result to the builder.
        /// </summary>
        /// <param name="transformValue">Function that adds any errors from its returned result, in case there are any.</param>
        /// <returns>Fluent interface self reference.</returns>
        public Builder Transform(Func<T, IResult<None, E>> transformValue) {
            if (!_hasValue) {
                throw new InvalidOperationException("Value is not set at time of transform.");
            }

            var result = transformValue.Invoke(_value);
            if (!result.Success) {
                _errors.AddRange(result.GetErrors());
            }

            return this;
        }

        /// <summary>
        /// Builds a Result object. If there are errors, returns a failed result. Otherwise, returns a successful result with value.
        /// </summary>
        /// <returns>Result object.</returns>
        /// <exception cref="InvalidOperationException">If the builder received no value nor errors.</exception>
        public Result<T, E> Build() {
            if (!_hasValue && _errors.Count == 0) {
                throw new InvalidOperationException("Result builder has no value or errors upon building.");
            }
            return _errors.Count == 0 ? Result<T, E>.Ok(_value) : Result<T, E>.Fail(_errors);
        }
    }

    readonly T _value = default!;
    readonly IReadOnlyCollection<E>? _errors;

    public bool Success { get; private init; }

    public bool Failure => !Success;

    public static Result<T, E> Ok(T value) {
        return new Result<T, E>(value);
    }

    public static Result<T, E> Fail(E error) {
        return new Result<T, E>(new List<E> { error });
    }

    public static Result<T, E> Fail(IReadOnlyCollection<E> errors) {
        return new Result<T, E>(errors);
    }

    public IResult<TNew, E> Then<TNew>(TNew value) {
        return Success ? Result<TNew, E>.Ok(value) : Result<TNew, E>.Fail(GetErrors());
    }

    public IResult<TNew, ENew> Then<TNew, ENew>(Func<T, IResult<TNew, ENew>> then) where ENew : IBaseException {
        return Success ? then.Invoke(_value) : Result<TNew, ENew>.Fail((IReadOnlyCollection<ENew>)GetErrors());
    }

    public IResult<TNew> Then<TNew>(Func<T, IResult<TNew>> then) {
        return Success ? then.Invoke(_value) : Result<TNew>.Fail((IReadOnlyCollection<IBaseException>)GetErrors());
    }

    public Task<IResult<TNew, ENew>> ThenAsync<TNew, ENew>(Func<T, Task<IResult<TNew, ENew>>> then) where ENew : IBaseException {
        if (Success) {
            return then.Invoke(_value);
        }
        IResult<TNew, ENew> result = Result<TNew, ENew>.Fail((IReadOnlyCollection<ENew>)GetErrors());
        return Task.FromResult(result);
    }

    public Task<IResult<TNew>> ThenAsync<TNew>(Func<T, Task<IResult<TNew>>> then) {
        if (Success) {
            return then.Invoke(_value);
        }
        IResult<TNew> result = Result<TNew>.Fail((IReadOnlyCollection<IBaseException>)GetErrors());
        return Task.FromResult(result);
    }

    public T Unwrap() {
        return Success ? _value : throw new InvalidOperationException("Result did not succeed, cannot access value.");
    }

    public IReadOnlyCollection<E> GetErrors() => _errors ?? throw new InvalidOperationException("Result wasn't a failure, cannot access errors.");

    protected Result(T value) {
        Success = true;
        _value = value;
    }

    protected Result(IReadOnlyCollection<E> errors) {
        Success = false;
        _errors = errors;
    }
}

/// <inheritdoc cref="Result{T, E}" />
public class Result<T> : Result<T, IBaseException>, IResult<T> {
    protected Result(T value) : base(value) { }

    protected Result(IReadOnlyCollection<IBaseException> errors) : base(errors) { }

    public static new Result<T> Ok(T value) {
        return new Result<T>(value);
    }

    public static new Result<T> Fail(IBaseException error) {
        return new Result<T>(new List<IBaseException> { error });
    }

    public static new Result<T> Fail(IReadOnlyCollection<IBaseException> errors) {
        return new Result<T>(errors);
    }

    public static Result<T> From(IResult<T, IBaseException> result) {
        return result.Success ? Result<T>.Ok(result.Unwrap()) : Result<T>.Fail(result.GetErrors());
    }
}
