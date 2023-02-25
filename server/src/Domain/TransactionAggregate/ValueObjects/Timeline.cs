using Domain.Common;
using Domain.Common.Exceptions;

namespace Domain.TransactionAggregate.ValueObjects;

public class Timeline : ValueObject {
    Frequency? _frequency;
    /// <summary>The frequency at which payments are made in the time period.</summary>
    public Frequency? Frequency {
        get => _frequency;
        private init {
            if (value is null && Period.IsRecurring) {
                throw new DomainException(new InvalidOperationException("Cannot set frequency to null with a payment end date."));
            } else if (value is not null && !Period.IsRecurring) {
                throw new DomainException(new InvalidOperationException("Cannot assign a frequency when there is no payment end date."));
            }
            _frequency = value;
        }
    }

    TimePeriod _period = null!;
    /// <summary>The time period within which payments are made. Lack of end date signifies a single-time payment.</summary>
    public TimePeriod Period {
        get => _period;
        private init {
            if (value.End is null && Frequency is not null) {
                throw new DomainException(new InvalidOperationException("Cannot set payment end date to null when there is a frequency."));
            } else if (value.End is not null && Frequency is null) {
                throw new DomainException(new InvalidOperationException("Cannot assign a payment end date when there is no frequency."));
            }
            _period = value;
        }
    }

    public Timeline(TimePeriod timePeriod) {
        Period = timePeriod;
    }

    public Timeline(TimePeriod period, Frequency frequency) {
        _period = period;
        Frequency = frequency;
    }

    private Timeline() { }

    protected override IEnumerable<object?> GetEqualityComponents() {
        yield return Frequency;
        yield return Period;
    }
}
