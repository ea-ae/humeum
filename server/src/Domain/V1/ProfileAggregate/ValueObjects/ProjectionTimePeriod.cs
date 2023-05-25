using Domain.Common.Exceptions;
using Domain.Common.Models;

using Shared.Interfaces;

using Shared.Models;

namespace Domain.V1.ProfileAggregate.ValueObjects;

/// <summary>
/// A time period with a start and end date.
/// </summary>
public class ProjectionTimePeriod : ValueObject {
    /// <summary>Start of time period.</summary>
    public DateOnly Start { get; private init; }

    /// <summary>End of time period.</summary>
    public DateOnly End { get; private init; }

    public static IResult<ProjectionTimePeriod, DomainException> Create(DateOnly start, DateOnly end) {
        if (end <= start) {
            return Result<ProjectionTimePeriod, DomainException>.Fail(new DomainException(new ArgumentException("Time period must end after it starts.")));
        }

        return Result<ProjectionTimePeriod, DomainException>.Ok(new ProjectionTimePeriod { Start = start, End = end });
    }

    ProjectionTimePeriod() { }

    protected override IEnumerable<object?> GetEqualityComponents() {
        yield return Start;
        yield return End;
    }
}
