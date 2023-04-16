using Domain.Common.Exceptions;
using Domain.Common.Interfaces;
using Domain.Common.Models;

namespace Domain.TransactionAggregate.ValueObjects;

/// <summary>
/// Time periods can be either instant or have a start and end date.
/// </summary>
public class TimePeriod : ValueObject {
    /// <summary>Start of time period.</summary>
    public DateOnly Start { get; private init; }

    /// <summary>End of time period. Lack of value signifies a singular time point (single-time payment).</summary>
    public DateOnly? End { get; private init; }

    /// <summary>Whether the payment is recurring or single-time.</summary>
    public bool IsRecurring => End is not null;

    public static IResult<TimePeriod, DomainException> Create(DateOnly timePoint) {
        return Result<TimePeriod, DomainException>.Ok(new TimePeriod { Start = timePoint });
    }

    public static IResult<TimePeriod, DomainException> Create(DateOnly start, DateOnly end) {
        if (end <= start) {
            return Result<TimePeriod, DomainException>.Fail(new DomainException(new ArgumentException("Time period must end after it starts.")));
        }

        return Result<TimePeriod, DomainException>.Ok(new TimePeriod { Start = start, End = end });
    }

    TimePeriod() { }

    protected override IEnumerable<object?> GetEqualityComponents() {
        yield return Start;
        yield return End;
    }
}
