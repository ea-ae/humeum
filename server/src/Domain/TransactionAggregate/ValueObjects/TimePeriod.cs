using Domain.Common;

namespace Domain.TransactionAggregate.ValueObjects;

public class TimePeriod : ValueObject
{
    public DateOnly Start { get; private set; } // todo make these just Date
    public DateOnly? End { get; private set; }

    public bool IsRecurring => End is not null;

    public TimePeriod(DateOnly timePoint)
    {
        Start = timePoint;
    }

    public TimePeriod(DateOnly start, DateOnly end) : this(start)
    {
        if (end <= start)
        {
            throw new ArgumentException("Time period must end after it starts.");
        }

        End = end;
    }

    private TimePeriod() { }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Start;
        yield return End;
    }
}
