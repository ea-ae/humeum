using Domain.Common;

namespace Domain.TransactionAggregate.ValueObjects;

public class TimePeriod : ValueObject {
    /// <summary>Start of time period.</summary>
    public DateOnly Start { get; private set; }

    /// <summary>End of time period. Lack of value signifies a singular time point (single-time payment).</summary>
    public DateOnly? End { get; private set; }

    /// <summary>Whether the payment is recurring or single-time.</summary>
    public bool IsRecurring => End is not null;

    public TimePeriod(DateOnly timePoint) {
        Start = timePoint;
    }

    public TimePeriod(DateOnly start, DateOnly end) : this(start) {
        if (end <= start) {
            throw new ArgumentException("Time period must end after it starts.");
        }
        End = end;
    }

    private TimePeriod() { }

    protected override IEnumerable<object?> GetEqualityComponents() {
        yield return Start;
        yield return End;
    }
}
