﻿using Domain.V1.AssetAggregate;
using Domain.V1.ProfileAggregate;
using Domain.V1.ProfileAggregate.ValueObjects;
using Domain.V1.TaxSchemeAggregate;
using Domain.V1.TaxSchemeAggregate.ValueObjects;
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

    [Fact]
    public void SimulateProjection_AssetPaymentsAndRegularPayment_ReturnsTimePointsWithInterest() {
        // arrange

        var asset = Asset.Create(1, "1", null, 100, 0, 1).Unwrap();
        var zeroAsset = Asset.Create(2, "2", null, 0, 0, 1).Unwrap();
        var transaction = BuildRecurringTransaction(15, new(1999, 1, 1), new(2004, 1, 5), TimeUnit.Years, 1, 1, TransactionType.Always, TaxScheme.NonTaxable);
        var assetTransaction = BuildRecurringTransaction(-10, new(1999, 1, 1), new(2004, 1, 1), TimeUnit.Years, 1, 2, TransactionType.Always, TaxScheme.NonTaxable, asset);
        var zeroAssetTransaction = BuildRecurringTransaction(-1, new(2000, 1, 1), new(2002, 1, 1), TimeUnit.Years, 1, 1, TransactionType.Always, TaxScheme.NonTaxable, zeroAsset);

        var expected = Projection.Create(new() {
            new(new(1999, 12, 31), 15, 0),
            new(new(2000, 12, 31), 19, 11),
            new(new(2001, 12, 31), 33, 22),
            new(new(2002, 12, 31), 38, 52),
            new(new(2003, 12, 31), 53, 102)
        }).Unwrap();

        // act

        var actual = CreateProjection(new[] { transaction, assetTransaction, zeroAssetTransaction }, new(1900, 1, 1), new(2100, 1, 1));

        // assert

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void SimulateProjection_AssetPaymentWithRetirementGoal_ReachesRetirementGoalAndWithdraws() {
        // arrange

        var asset = Asset.Create(1, "1", null, 100, 0, 1).Unwrap();
        var initialTransaction = BuildOneTimeTransaction(20, new(2000, 1, 1), TransactionType.Always, TaxScheme.NonTaxable);
        var assetTransaction = BuildRecurringTransaction(-10, new(2000, 1, 1), new(2006, 1, 1), TimeUnit.Years, 1, 1, TransactionType.PreRetirementOnly, TaxScheme.NonTaxable, asset);
        var recurringPayment = BuildRecurringTransaction(-25, new(2000, 1, 1), new(2006, 1, 1), TimeUnit.Years, 1, 1, TransactionType.RetirementOnly, TaxScheme.NonTaxable);

        var expected = Projection.Create(new() {
            new(new(2000, 1, 1), 20, 0),
            new(new(2000, 12, 31), 10, 10),
            new(new(2001, 12, 31), 0, 30),
            new(new(2002, 12, 31), 0, 60),
            new(new(2003, 12, 31), 0, 120),
            new(new(2004, 12, 31), 0, 215.46), // withdraw 25 for retirement payments from now onward (leap year added .46)
            new(new(2005, 12, 31), 0, 405.92)
        }).Unwrap();

        // act

        var actual = CreateProjection(new[] { initialTransaction, assetTransaction, recurringPayment }, new(1900, 1, 1), new(2010, 1, 1), goal: 100, withdrawal: 10);

        // assert

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void SimulateProjection_AssetPaymentsOfDifferingTaxAndRiskPriority_WithdrawsAssetsInOrder() {
        // arrange

        var lowAsset = Asset.Create(1, "1", null, 100, 0, 1).Unwrap(); // low risk, high return (non-taxable, thus we can withdraw anytime without pressure)
        var highAsset = Asset.Create(1, "1", null, 0, 30, 1).Unwrap(); // high risk, low return (attached to age requirement tax scheme)
        var ageTaxScheme = BuildAgeLimitTaxScheme();

        var initialTransaction = BuildOneTimeTransaction(15, new(2057, 1, 1), TransactionType.Always, TaxScheme.NonTaxable);
        var boostTransaction = BuildOneTimeTransaction(10, new(2060, 1, 1), TransactionType.Always, TaxScheme.NonTaxable);
        var lowTransaction = BuildRecurringTransaction(-10, new(2057, 1, 1), new(2061, 1, 1), TimeUnit.Years, 1, 1,
                                                       TransactionType.Always, TaxScheme.NonTaxable, lowAsset);
        var highTransaction = BuildRecurringTransaction(-4, new(2057, 1, 1), new(2061, 1, 1), TimeUnit.Years, 1, 1,
                                                        TransactionType.Always, ageTaxScheme,
                                                        highAsset);

        var expected = Projection.Create(new() {
            new(new(2057, 1, 1), 15, 0),
            new(new(2057, 12, 31), 1, 10 + 4), // 10 in low-risk, 4 in high-risk (low-risk 10 will accumulate to 20)
            new(new(2058, 12, 31), 0, 17 + 8), // 20-3=17 in low-risk, 8 in high-risk (sold low-risk asset due to tax scheme priority)
            new(new(2059, 12, 31), 0, 30 + 12), // 34-4=30 in low-risk, 12 in high-risk (sold low-risk asset due to tax scheme priority)
            new(new(2060, 1, 1), 10, 30.06 + 12), // liquid income boost (incl mid-year additions, leap year is coming)
            new(new(2060, 12, 31), 0, 74.12 + 8), // 60+10+4=74 in low-risk, 12-4=8 in high-risk (sold high-risk asset due to tax age requirement being reached)
        }).Unwrap();

        // act

        var actual = CreateProjection(new[] { initialTransaction, boostTransaction, lowTransaction, highTransaction }, new(1900, 1, 1), new(2100, 1, 1));

        // assert

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void SimulateProjection_AssetWithdrawalWithIncomeTax_WithdrawsWithTax() {
        // act

        var asset = Asset.Create(1, "1", null, 100, 0, 1).Unwrap();

        var initialTransaction = BuildOneTimeTransaction(62.5m, new(2000, 1, 1), TransactionType.Always, TaxScheme.IncomeTax);
        var assetTransaction = BuildOneTimeTransaction(-50, new(2001, 12, 31), TransactionType.Always, TaxScheme.IncomeTax, asset);
        var recurringPayment = BuildRecurringTransaction(-40, new(2002, 1, 1), new(2004, 1, 1), TimeUnit.Years, 1, 1, TransactionType.Always, TaxScheme.NonTaxable);

        var expected = Projection.Create(new() {
            new(new(2000, 1, 1), 62.5 - 12.5, 0), // 20% income tax => 50
            new(new(2001, 12, 31), 0, 50), // tax-free: 50, value: 50
            new(new(2002, 12, 31), 0, 50 + 50 - 40), // tax-free: 10, value: 60
            new(new(2003, 12, 31), 0, 60 + 60 - 40 - 6), // tax-free: 0, taxed: (40 - 10) * 0.2 = 6, value: 74
        }).Unwrap();

        // act

        var actual = CreateProjection(new[] { initialTransaction, assetTransaction, recurringPayment }, new(1900, 1, 1), new(2100, 1, 1));

        // assert

        Assert.Equal(expected, actual);
    }

    public static Projection CreateProjection(IEnumerable<Transaction> transactions, DateOnly start, DateOnly end, double goal = 100_000, double withdrawal = 3.5) {
        var simulator = ProjectionSimulator.Create(transactions, ProjectionTimePeriod.Create(start, end).Unwrap()).Unwrap();
        return simulator.SimulateProjection(goal, withdrawal, new(2000, 1, 1)).Unwrap();
    }

    /// <summary>Shorthand for constructing a single-time transaction.</summary>
    public static Transaction BuildOneTimeTransaction(decimal amount, DateOnly time, TransactionType type, TaxScheme taxScheme, Asset? asset = null) {
        var profile = Profile.Create(1, "", retirementGoal: 500_000, withdrawalRate: 3.5m, birthday: new DateOnly(2000, 1, 1)).Unwrap();
        var timeline = Timeline.Create(TimePeriod.Create(time).Unwrap()).Unwrap();
        var transaction = Transaction.Create(null, null, amount, type, timeline, profile, taxScheme, asset).Unwrap();
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
                                                        TaxScheme taxScheme,
                                                        Asset? asset = null) {
        var profile = Profile.Create(1, "", retirementGoal: 500_000, withdrawalRate: 3.5m, birthday: new DateOnly(2000, 1, 1)).Unwrap();
        var frequency = Frequency.Create(timeUnit, nTimes, everyN).Unwrap();
        var timeline = Timeline.Create(TimePeriod.Create(start, end).Unwrap(), frequency).Unwrap();
        var transaction = Transaction.Create(null, null, amount, type, timeline, profile, taxScheme, asset).Unwrap();
        return transaction;
    }

    public static TaxScheme BuildAgeLimitTaxScheme() {
        var incentiveScheme = TaxIncentiveScheme.Create(20, 60, null, null).Unwrap();
        var taxScheme = TaxScheme.Create("a", "b", 20, incentiveScheme).Unwrap();
        return taxScheme;
    }
}
