using Domain.Common;
using Domain.Entities;

namespace Domain.ValueObjects;

public class Timeline : ValueObject {
    Frequency? _frequency;
    public Frequency? Frequency {
        get => _frequency;
        private set {
            if (value is null && TimePeriod.IsRecurring) {
                throw new InvalidOperationException("Cannot set frequency to null with a payment end date.");
            } else if (value is not null && !TimePeriod.IsRecurring) {
                throw new InvalidOperationException("Cannot assign a frequency when there is no payment end date.");
            }
            _frequency = value;
        }
    }

    TimePeriod _timePeriod = null!;
    public TimePeriod TimePeriod {
        get => _timePeriod;
        set {
            if (value.End is null && Frequency is not null) {
                throw new InvalidOperationException("Cannot set payment end date to null when there is a frequency.");
            } else if (value.End is not null && Frequency is null) {
                throw new InvalidOperationException("Cannot assign a payment end date when there is no frequency.");
            }
            _timePeriod = value;
        }
    }

    public Timeline(TimePeriod timePeriod) {
        TimePeriod = timePeriod;
    }

    public Timeline(TimePeriod timePeriod, Frequency frequency) {
        _timePeriod = timePeriod;
        Frequency = frequency;
    }

    private Timeline() { }

    protected override IEnumerable<object?> GetEqualityComponents() {
        yield return Frequency;
    }
}
