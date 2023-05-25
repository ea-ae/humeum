using Domain.V1.ProfileAggregate;
using Domain.V1.TaxSchemeAggregate;
using Domain.V1.TransactionAggregate;
using Domain.V1.TransactionAggregate.ValueObjects;

using Xunit;

namespace Domain.Test.ProfileAggregate;

public class ProjectionSimulatorTest {
    [Fact]
    public void SimulateProjection_OnePayment_ReturnsOneTimePoint() {
        // arrange

        //var transaction = BuildOneTimeTransaction(10, new DateOnly(2000, 1, 1), TransactionType.Always, 
        //ProjectionSimulator.Create)
    }

    /// <summary>Shorthand for constructing a single-time transaction.</summary>
    public static Transaction BuildOneTimeTransaction(decimal amount, DateOnly time, TransactionType type, TaxScheme taxScheme) {
        var profile = Profile.Create(1, "").Unwrap();
        var timeline = Timeline.Create(TimePeriod.Create(time).Unwrap()).Unwrap();
        var transaction = Transaction.Create(null, null, amount, type, timeline, profile, taxScheme).Unwrap();
        return transaction;
    }

    /// <summary>Shorthand for constructing a recurring transaction.</summary>
    public static Transaction BuildRecurringTransaction(decimal amount, DateOnly start, DateOnly end, TransactionType type, TaxScheme taxScheme) {
        var profile = Profile.Create(1, "").Unwrap();
        var timeline = Timeline.Create(TimePeriod.Create(start, end).Unwrap()).Unwrap();
        var transaction = Transaction.Create(null, null, amount, type, timeline, profile, taxScheme).Unwrap();
        return transaction;
    }
}
