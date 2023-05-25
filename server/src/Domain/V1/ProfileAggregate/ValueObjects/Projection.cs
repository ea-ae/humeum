using Domain.Common.Exceptions;
using Domain.Common.Models;

using Shared.Interfaces;
using Shared.Models;

namespace Domain.V1.ProfileAggregate.ValueObjects;

public struct TimePoint {
    public DateOnly Date;
    public double LiquidWorth;
    public double AssetWorth;

    public TimePoint(DateOnly date, double liquidWorth, double assetWorth) {
        Date = date;
        LiquidWorth = liquidWorth;
        AssetWorth = assetWorth;
    }

    public override string ToString() {
        return $"{Date.ToShortDateString()} L{LiquidWorth:F0} A{AssetWorth:F0}";
    }
}

public class Projection : ValueObject {
    public List<TimePoint> TimeSeries { get; private init; } = null!;

    public static IResult<Projection, DomainException> Create(List<TimePoint> timeSeries) {
        var projection = new Projection() { TimeSeries = timeSeries };
        return Result<Projection, DomainException>.Ok(projection);
    }

    Projection() { }

    protected override IEnumerable<object?> GetEqualityComponents() {
        foreach (var timePoint in TimeSeries) {
            yield return timePoint;
        }
    }
}
