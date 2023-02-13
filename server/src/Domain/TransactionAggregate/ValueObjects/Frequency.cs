﻿using Domain.Common;

namespace Domain.TransactionAggregate.ValueObjects;

public class Frequency : ValueObject {
    public int TimeUnitId { get; private set; }
    /// <summary>Time unit used to determine cycle length.</summary>
    public TimeUnit TimeUnit { get; private set; } = null!;

    int _timesPerCycle;
    /// <summary>How many times per cycle the payment is made.</summary>
    public int TimesPerCycle {
        get => _timesPerCycle;
        private set {
            if (value <= 0) {
                throw new ArgumentException("Times per period must be greater than zero.");
            }
            _timesPerCycle = value;
        }
    }

    int _unitsInCycle;
    /// <summary>How many time units a single cycle lasts.</summary>
    public int UnitsInCycle {
        get => _unitsInCycle;
        private set {
            if (value <= 0) {
                throw new ArgumentException("Units in period must be greater than zero.");
            }
            _unitsInCycle = value;
        }
    }

    public Frequency(TimeUnit unit, int timesPerCycle, int unitsInCycle) {
        TimeUnitId = unit.Id;
        TimeUnit = unit;
        TimesPerCycle = timesPerCycle;
        UnitsInCycle = unitsInCycle;
    }

    private Frequency() { }

    protected override IEnumerable<object?> GetEqualityComponents() {
        yield return TimeUnit;
        yield return TimesPerCycle;
        yield return UnitsInCycle;
    }
}
