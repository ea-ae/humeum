using Domain.Common.Exceptions;
using Domain.Common.Models;

using Shared.Interfaces;
using Shared.Models;

namespace Domain.V1.TransactionAggregate.ValueObjects;

public class Timeline : ValueObject
{
    /// <summary>The time period within which payments are made. Lack of end date signifies a single-time payment.</summary>
    public TimePeriod Period { get; private init; } = null!;

    /// <summary>The frequency at which payments are made in the time period.</summary>
    public Frequency? Frequency { get; private init; }

    /// <summary>Create a timeline that represents a single time point.</summary>
    public static IResult<Timeline, IBaseException> Create(TimePeriod period)
    {
        if (period.End is not null)
        {
            var error = new DomainException(new InvalidOperationException("Cannot assign a payment end date when there is no frequency."));
            return Result<Timeline, IBaseException>.Fail(error);
        }

        return Result<Timeline, IBaseException>.Ok(new Timeline() { Period = period });
    }

    /// <summary>Create a timeline that represents a time period with a specified density of time points (aka frequency) within.</summary
    public static IResult<Timeline, IBaseException> Create(TimePeriod period, Frequency frequency)
    {
        if (period.End is null)
        {
            var error = new DomainException(new InvalidOperationException("Payment end date must be set when there is a frequency."));
            return Result<Timeline, IBaseException>.Fail(error);
        }

        return Result<Timeline, IBaseException>.Ok(new Timeline() { Period = period, Frequency = frequency });
    }

    Timeline() { }

    public IResult<IEnumerable<DateOnly>, DomainException> GetPaymentDates() {
        if (Frequency is null) {
            return Result<IEnumerable<DateOnly>, DomainException>.Ok(new List<DateOnly>() { Period.Start });
        }

        List<DateOnly> dates = new();
        DateOnly cursor = Period.Start.AddDays(-1);

        while (cursor <= Period.End) {
            DateOnly dateAfterCycle;

            // get the date after the cycle
            if (Frequency.TimeUnit == TimeUnit.Days) {
                dateAfterCycle = cursor.AddDays(Frequency.UnitsInCycle);
            } else if (Frequency.TimeUnit == TimeUnit.Weeks) {
                dateAfterCycle = cursor.AddDays(Frequency.UnitsInCycle * 7);
            } else if (Frequency.TimeUnit == TimeUnit.Months) {
                dateAfterCycle = cursor.AddMonths(Frequency.UnitsInCycle);
            } else if (Frequency.TimeUnit == TimeUnit.Years) {
                dateAfterCycle = cursor.AddYears(Frequency.UnitsInCycle);
            } else {
                throw new NotImplementedException();
            }

            // get cycle length in days
            int cycleLength = dateAfterCycle.DayNumber - cursor.DayNumber;

            // get amount of days in a single frequency period of the cycle
            int daysInCyclePeriod = (int)Math.Ceiling((double)cycleLength / Frequency.TimesPerCycle);

            // add date points, making sure the numbers add up to the cycle length
            // e.g. 3 payments over a week would mean payments on days 3/6/7
            for (int daysRemaining = cycleLength; daysRemaining > 0; daysRemaining -= daysInCyclePeriod) {
                if (daysRemaining < daysInCyclePeriod) {
                    daysInCyclePeriod = daysRemaining; // add the remaining days to the last period
                }

                cursor = cursor.AddDays(daysInCyclePeriod);

                if (cursor > Period.End) {
                    break;
                }

                dates.Add(cursor);
            }
        }

        return Result<IEnumerable<DateOnly>, DomainException>.Ok(dates);
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Frequency;
        yield return Period;
    }
}
