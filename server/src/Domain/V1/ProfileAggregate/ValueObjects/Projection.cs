using Domain.Common.Exceptions;
using Domain.Common.Models;
using Domain.V1.TransactionAggregate;

using Shared.Interfaces;
using Shared.Models;

namespace Domain.V1.ProfileAggregate.ValueObjects;

public class Projection : ValueObject {
    /// <summary>Projection of net worth in a given time period.</summary>
    public IEnumerable<decimal>? NetWorthProjection { get; private init; }

    public static IResult<Projection, DomainException> Create(IEnumerable<Transaction> transactions, TimePeriod period) {
        return Result<Projection, DomainException>.Ok(new Projection(transactions, period));
    }

    Projection(IEnumerable<Transaction> transactions, TimePeriod period) {
        var earliestTransaction = transactions.Select(t => t.PaymentTimeline.Period.Start).Min();
        throw new NotImplementedException();
    }

    protected override IEnumerable<object?> GetEqualityComponents() {
        yield return NetWorthProjection;
    }
}
