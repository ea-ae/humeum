using Domain.V1.ProfileAggregate;
using Domain.V1.ProfileAggregate.ValueObjects;
using Domain.V1.TaxSchemeAggregate;
using Domain.V1.TransactionAggregate;
using Domain.V1.TransactionAggregate.ValueObjects;

using Xunit;

namespace Domain.Test.ProfileAggregate;

public class ProjectionSimulatorTest {
    [Fact]
    public void SimulateProjection_OnePayment_ReturnsOneTimePoint() {
        // arrange

        var transaction = BuildOneTimeTransaction(10, new DateOnly(2000, 1, 1), TransactionType.Always, TaxScheme.NonTaxable);
        var expected = Projection.Create(new() { new(new DateOnly(2000, 1, 1), 10, 0) }).Unwrap();

        // act

        var actual = CreateProjection(new[] { transaction }, new DateOnly(1900, 1, 1), new DateOnly(2100, 1, 1));

        // assert

        Assert.Equal(expected, actual);
    }

    public static Projection CreateProjection(IEnumerable<Transaction> transactions, DateOnly start, DateOnly end) {
        var simulator = ProjectionSimulator.Create(transactions, ProjectionTimePeriod.Create(start, end).Unwrap()).Unwrap();
        return simulator.SimulateProjection(500_000, 3.5).Unwrap();
    }

    /// <summary>Shorthand for constructing a single-time transaction.</summary>
    public static Transaction BuildOneTimeTransaction(decimal amount, DateOnly time, TransactionType type, TaxScheme taxScheme) {
        var profile = Profile.Create(1, "", retirementGoal: 500_000, withdrawalRate: 3.5m, birthday: new DateOnly(2000, 1, 1)).Unwrap();
        var timeline = Timeline.Create(TimePeriod.Create(time).Unwrap()).Unwrap();
        var transaction = Transaction.Create(null, null, amount, type, timeline, profile, taxScheme).Unwrap();
        return transaction;
    }

    /// <summary>Shorthand for constructing a recurring transaction.</summary>
    public static Transaction BuildRecurringTransaction(decimal amount, DateOnly start, DateOnly end, TransactionType type, TaxScheme taxScheme) {
        var profile = Profile.Create(1, "", retirementGoal: 500_000, withdrawalRate: 3.5m, birthday: new DateOnly(2000, 1, 1)).Unwrap();
        var timeline = Timeline.Create(TimePeriod.Create(start, end).Unwrap()).Unwrap();
        var transaction = Transaction.Create(null, null, amount, type, timeline, profile, taxScheme).Unwrap();
        return transaction;
    }
}
