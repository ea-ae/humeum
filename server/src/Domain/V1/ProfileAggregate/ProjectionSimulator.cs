﻿using Domain.Common.Exceptions;
using Domain.V1.ProfileAggregate.ValueObjects;
using Domain.V1.TransactionAggregate;
using Domain.V1.TransactionAggregate.ValueObjects;

using Shared.Interfaces;
using Shared.Models;

namespace Domain.V1.ProfileAggregate;

public class ProjectionSimulator {
    struct PaymentAsset {
        public double ReturnRate;
        public double StandardDeviation;
    }

    struct Payment {
        public DateOnly Date;
        public double Amount;
        public PaymentAsset? Asset;
        public bool PreRetirement;
        public bool PostRetirement;
    }

    /// <summary>Projection of net worth in a given time period.</summary>
    public IEnumerable<decimal>? NetWorthProjection { get; private init; }

    /// <summary>List of chronological payments processed from the transaction list.</summary>
    List<Payment> _payments;

    public static IResult<ProjectionSimulator, DomainException> Create(IEnumerable<Transaction> transactions, ProjectionTimePeriod period) {
        var earliestTransaction = transactions.Select(t => t.PaymentTimeline.Period.Start).Min();
        return GetPaymentChronology(transactions, period).Then(payments =>
        {
            return Result<ProjectionSimulator, DomainException>.Ok(new ProjectionSimulator(payments));
        });
    }

    /// <summary>Returns a timeline of payments in chronological order.</summary>
    static IResult<List<Payment>, DomainException> GetPaymentChronology(IEnumerable<Transaction> transactions, ProjectionTimePeriod period) {
        List<Payment> payments = new();
        foreach (var transaction in transactions)
        {
            var paymentDates = transaction.GetPaymentDates(period.Start, period.End);
            if (paymentDates.Failure)
            {
                return Result<List<Payment>, DomainException>.Fail(paymentDates.GetErrors());
            }
            payments.AddRange(paymentDates.Unwrap().Select(pd => new Payment {
                Date = pd,
                Amount = (double)transaction.Amount,
                Asset = transaction.Asset is null ? null : new PaymentAsset {
                    ReturnRate = (double)transaction.Asset.ReturnRate / 100,
                    StandardDeviation = (double)transaction.Asset.StandardDeviation / 100
                },
                PreRetirement = transaction.Type != TransactionType.RetirementOnly,
                PostRetirement = transaction.Type != TransactionType.PreRetirementOnly
            }))
            ;;
        }

        payments.Sort((p1, p2) => p1.Date.CompareTo(p2.Date));
        return Result<List<Payment>, DomainException>.Ok(payments);
    }

    ProjectionSimulator(List<Payment> payments) {
        _payments = payments;
    }

    /// <summary>Simulates a projection based on constructed timeline of payments.</summary>
    /// <param name="retirementGoal">A liquid and asset worth sum goalpost for the retirement phase.</param>
    /// <param name="withdrawalRate">Withdrawal rate of assets from portfolio into liquid value, in descending standard deviation order (reducing risk).</param>
    /// <returns></returns>
    public IResult<Projection, DomainException> SimulateProjection(double retirementGoal, double withdrawalRate, DateOnly birthday) {
        List<TimePoint> series = new();

        if (!_payments.Any()) {
            return Projection.Create(series);
        }

        DateOnly? retiringAt = null;
        double liquidBalance = 0;
        double assetBalance = 0;

        Dictionary<PaymentAsset, double> assets = new();
        var date = _payments.First().Date.AddDays(-1);

        foreach (var payment in _payments) {
            // check whether a payment should be processed pre/post-retirement phase
            if (!payment.PreRetirement || !payment.PostRetirement) {
                bool activePreRetirement = payment.PreRetirement && (retiringAt is null || date == retiringAt); // enables determinism irrespective of same-day ordering
                bool activePostRetirement = payment.PostRetirement && retiringAt is not null && date != retiringAt;

                if (!activePreRetirement && !activePostRetirement) {
                    continue;
                }
            }

            double yearsPassed = (double)(payment.Date.DayNumber - date.DayNumber) / 365;

            // check whether retirement goal has been reached
            if (yearsPassed > 0 && retiringAt is null && liquidBalance + assetBalance >= retirementGoal) {
                retiringAt = date;
            }

            // add asset interest
            assetBalance += AddAssetInterest(assets, yearsPassed);

            // add payments
            liquidBalance += payment.Amount;
            if (payment.Asset is not null) {
                var asset = (PaymentAsset)payment.Asset;
                assets.TryGetValue(asset, out double amount);
                assets[asset] = amount - payment.Amount;
                assetBalance -= payment.Amount;
            }

            // in case we are in debt, withdraw from assets to reach zero
            if (liquidBalance < 0) {
                var withdrawn = WithdrawAssets(assets, -liquidBalance);
                liquidBalance += withdrawn;
                assetBalance -= withdrawn;
            }

            // update date and add time point to series
            if (yearsPassed > 0) {
                date = payment.Date;
                series.Add(new TimePoint(date, Math.Ceiling(liquidBalance * 100) / 100, Math.Ceiling(assetBalance * 100) / 100));
            } else {
                series[^1] = new TimePoint(date, Math.Ceiling(liquidBalance * 100) / 100, Math.Ceiling(assetBalance * 100) / 100);
            }
        }

        return Projection.Create(series);
    }

    /// <summary>Adds asset interests to their value, re-investing all income.</summary>
    /// <param name="assets">Dictionary of assets and their values.</param>
    /// <param name="yearsPassed">Over what time span the ROI will be applied.</param>
    /// <returns>Total earnings over time period in terms of asset worth.</returns>
    double AddAssetInterest(Dictionary<PaymentAsset, double> assets, double yearsPassed) {
        if (yearsPassed == 0) {
            return 0;
        }

        double earnings = 0;

        foreach ((PaymentAsset asset, double amount) in assets) {
            double newAmount = amount * Math.Pow(1 + asset.ReturnRate, yearsPassed); // simple version for now, no SD involved

            earnings += newAmount - amount;
            assets[asset] = newAmount;
        }

        return earnings;
    }

    /// <summary>
    /// Withdraws assets, prioritizing the withdrawal of higher-risk assets. Used when in debt, usually retirement.
    /// </summary>
    /// <param name="assets">Dictionary of assets and their values.</param>
    /// <param name="amount">Amount to withdraw if possible.</param>
    /// <returns>Amount withdrawn.</returns>
    double WithdrawAssets(Dictionary<PaymentAsset, double> assets, double withdrawalAmount) {
        double withdrawn = 0;

        foreach ((PaymentAsset asset, double amount) in assets.OrderBy(a => -a.Key.StandardDeviation)) {
            double leftToWithdraw = withdrawalAmount - withdrawn;

            if (withdrawalAmount < amount) {
                withdrawn += withdrawalAmount;
                assets[asset] -= withdrawalAmount;
                break;
            } else {
                withdrawn += assets[asset];
                assets.Remove(asset); // asset has been emptied
            }
        }

        return withdrawn;
    }

    /// <summary>Calculate age, taking into account leap years. Used primarily for tax calculations.</summary>
    int CalculateAge(DateOnly birthday, DateOnly now) {
        var age = now.Year - birthday.Year;
        return birthday > now.AddYears(-age) ? age - 1 : age;
    }
}
