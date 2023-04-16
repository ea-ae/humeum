using Domain.Common.Exceptions;
using Domain.Common.Models;

using Shared.Interfaces;
using Shared.Models;

namespace Domain.TransactionAggregate.ValueObjects;

public class Timeline : ValueObject {
    /// <summary>The time period within which payments are made. Lack of end date signifies a single-time payment.</summary>
    public TimePeriod Period { get; private init; } = null!;

    /// <summary>The frequency at which payments are made in the time period.</summary>
    public Frequency? Frequency { get; private init; }

    /// <summary>Create a timeline that represents a single time point.</summary>
    public static IResult<Timeline, IBaseException> Create(TimePeriod period) {
        if (period.End is not null) {
            var error = new DomainException(new InvalidOperationException("Cannot assign a payment end date when there is no frequency."));
            return Result<Timeline, IBaseException>.Fail(error);
        }

        return Result<Timeline, IBaseException>.Ok(new Timeline() { Period = period });
    }

    /// <summary>Create a timeline that represents a time period with a specified density of time points (aka frequency) within.</summary
    public static IResult<Timeline, IBaseException> Create(TimePeriod period, Frequency frequency) {
        if (period.End is null) {
            var error = new DomainException(new InvalidOperationException("Payment end date must be set when there is a frequency."));
            return Result<Timeline, IBaseException>.Fail(error);
        }

        return Result<Timeline, IBaseException>.Ok(new Timeline() { Period = period, Frequency = frequency });
    }

    Timeline() { }

    protected override IEnumerable<object?> GetEqualityComponents() {
        yield return Frequency;
        yield return Period;
    }
}
