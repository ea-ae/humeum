using Domain.Common;

namespace Domain.TransactionAggregate.ValueObjects;

public class Timeline : ValueObject
{
    Frequency? _frequency;
    public Frequency? Frequency
    {
        get => _frequency;
        private set
        {
            if (value is null && Period.IsRecurring)
            {
                throw new InvalidOperationException("Cannot set frequency to null with a payment end date.");
            }
            else if (value is not null && !Period.IsRecurring)
            {
                throw new InvalidOperationException("Cannot assign a frequency when there is no payment end date.");
            }
            _frequency = value;
        }
    }

    TimePeriod _period = null!;
    public TimePeriod Period
    {
        get => _period;
        private set
        {
            if (value.End is null && Frequency is not null)
            {
                throw new InvalidOperationException("Cannot set payment end date to null when there is a frequency.");
            }
            else if (value.End is not null && Frequency is null)
            {
                throw new InvalidOperationException("Cannot assign a payment end date when there is no frequency.");
            }
            _period = value;
        }
    }

    public Timeline(TimePeriod timePeriod)
    {
        Period = timePeriod;
    }

    public Timeline(TimePeriod period, Frequency frequency)
    {
        _period = period;
        Frequency = frequency;
    }

    private Timeline() { }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Frequency;
    }
}
