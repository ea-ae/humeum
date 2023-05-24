using Domain.Common.Exceptions;
using Domain.Common.Models;

using Shared.Interfaces;
using Shared.Models;

namespace Domain.V1.TransactionAggregate.ValueObjects;

public class Frequency : ValueObject
{
    /// <summary>How many times per cycle the payment is made.</summary>
    public int TimesPerCycle { get; private init; }

    /// <summary>How many time units a single cycle lasts.</summary>
    public int UnitsInCycle { get; private init; }

    public int TimeUnitId { get; private init; }
    /// <summary>Time unit used to determine cycle length.</summary>
    public TimeUnit TimeUnit { get; private init; } = null!;

    public static IResult<Frequency, IBaseException> Create(TimeUnit unit, int timesPerCycle, int unitsInCycle)
    {
        var builder = new Result<Frequency, IBaseException>.Builder();

        if (timesPerCycle <= 0)
        {
            builder.AddError(new DomainException(new ArgumentException("Times per period/cycle must be greater than zero.")));
        }

        if (unitsInCycle <= 0)
        {
            builder.AddError(new DomainException(new ArgumentException("Units in period/cycle must be greater than zero.")));
        }

        var frequency = new Frequency() { TimesPerCycle = timesPerCycle, UnitsInCycle = unitsInCycle, TimeUnit = unit, TimeUnitId = unit.Id };
        return builder.AddValue(frequency).Build();
    }

    Frequency() { }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return TimeUnitId;
        yield return TimesPerCycle;
        yield return UnitsInCycle;
    }
}
