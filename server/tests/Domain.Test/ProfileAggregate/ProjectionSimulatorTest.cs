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

    [Fact]
    public void SimulateProjection_OneRecurringDailyPayment_ReturnsTimePointsWithIncreasingBalance() {
        // arrange

        var transaction = BuildRecurringTransaction(10, new(2000, 1, 1), new(2000, 1, 3), TimeUnit.Days, 1, 1, TransactionType.Always, TaxScheme.NonTaxable);
        var expected = Projection.Create(new() { 
            new(new(2000, 1, 1), 10, 0),
            new(new(2000, 1, 2), 20, 0),
            new(new(2000, 1, 3), 30, 0)
        }).Unwrap();

        // act

        var actual = CreateProjection(new[] { transaction }, new(1900, 1, 1), new(2100, 1, 1));

        // assert

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void SimulateProjection_TwoRecurringPayments_ReturnsTimePointsWithIncreasingBalance() {
        // arrange

        var dailyTransaction = BuildRecurringTransaction(10, new(2000, 1, 1), new(2000, 1, 3), TimeUnit.Days, 1, 1, TransactionType.Always, TaxScheme.NonTaxable);
        var biDailyTransaction = BuildRecurringTransaction(-3, new(2000, 1, 1), new(2000, 1, 3), TimeUnit.Days, 1, 2, TransactionType.Always, TaxScheme.NonTaxable);
        var expected = Projection.Create(new() {
            new(new(2000, 1, 1), 10, 0),
            new(new(2000, 1, 2), 17, 0),
            new(new(2000, 1, 3), 27, 0)
        }).Unwrap();

        // act

        var actual = CreateProjection(new[] { dailyTransaction, biDailyTransaction }, new(1900, 1, 1), new(2100, 1, 1));

        // assert

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void SimulateProjection_ThreePaymentsOfDifferentTypes_ReturnsTimePointsFilteredByRetirementPhase() {
        // arrange

        var alwaysTransaction = BuildRecurringTransaction(10, new(2000, 1, 1), new(2000, 1, 4), TimeUnit.Days, 1, 1, TransactionType.Always, TaxScheme.NonTaxable);
        var preTransaction = BuildRecurringTransaction(-3, new(2000, 1, 2), new(2000, 1, 5), TimeUnit.Days, 1, 1, TransactionType.PreRetirementOnly, TaxScheme.NonTaxable);
        var postTransaction = BuildRecurringTransaction(-1, new(2000, 1, 1), new(2000, 1, 5), TimeUnit.Days, 1, 1, TransactionType.RetirementOnly, TaxScheme.NonTaxable);
        var expected = Projection.Create(new() {
            new(new(2000, 1, 1), 10, 0),
            new(new(2000, 1, 2), 17, 0),
            new(new(2000, 1, 3), 24, 0),
            new(new(2000, 1, 4), 33, 0),
            new(new(2000, 1, 5), 32, 0),
        }).Unwrap();

        // act

        var actual = CreateProjection(new[] { alwaysTransaction, preTransaction, postTransaction }, new(2000, 1, 1), new(2100, 1, 1), goal: 18);

        // assert

        Assert.Equal(expected, actual);
    }

    public static Projection CreateProjection(IEnumerable<Transaction> transactions, DateOnly start, DateOnly end, double goal = 100_000, double withdrawal = 3.5) {
        var simulator = ProjectionSimulator.Create(transactions, ProjectionTimePeriod.Create(start, end).Unwrap()).Unwrap();
        return simulator.SimulateProjection(goal, withdrawal).Unwrap();
    }

    /// <summary>Shorthand for constructing a single-time transaction.</summary>
    public static Transaction BuildOneTimeTransaction(decimal amount, DateOnly time, TransactionType type, TaxScheme taxScheme) {
        var profile = Profile.Create(1, "", retirementGoal: 500_000, withdrawalRate: 3.5m, birthday: new DateOnly(2000, 1, 1)).Unwrap();
        var timeline = Timeline.Create(TimePeriod.Create(time).Unwrap()).Unwrap();
        var transaction = Transaction.Create(null, null, amount, type, timeline, profile, taxScheme).Unwrap();
        return transaction;
    }

    /// <summary>Shorthand for constructing a recurring transaction.</summary>
    public static Transaction BuildRecurringTransaction(decimal amount,
                                                        DateOnly start,
                                                        DateOnly end,
                                                        TimeUnit timeUnit,
                                                        int nTimes,
                                                        int everyN,
                                                        TransactionType type,
                                                        TaxScheme taxScheme) {
        var profile = Profile.Create(1, "", retirementGoal: 500_000, withdrawalRate: 3.5m, birthday: new DateOnly(2000, 1, 1)).Unwrap();
        var frequency = Frequency.Create(timeUnit, nTimes, everyN).Unwrap();
        var timeline = Timeline.Create(TimePeriod.Create(start, end).Unwrap(), frequency).Unwrap();
        var transaction = Transaction.Create(null, null, amount, type, timeline, profile, taxScheme).Unwrap();
        return transaction;
    }
}
