using Domain.Common.Exceptions;
using Domain.V1.ProfileAggregate.ValueObjects;
using Domain.V1.TaxSchemeAggregate;
using Domain.V1.TransactionAggregate;
using Domain.V1.TransactionAggregate.ValueObjects;

using Shared.Interfaces;
using Shared.Models;

namespace Domain.V1.ProfileAggregate;

public class ProjectionSimulator {
    struct PaymentAsset {
        public double ReturnRate;
        public double StandardDeviation;
        public TaxScheme TaxScheme;
    }

    struct Payment {
        public DateOnly Date;
        public double Amount;
        public PaymentAsset? Asset;
        public TaxScheme TaxScheme;

        public bool PreRetirement;
        public bool PostRetirement;
    }

    /// <summary>Projection of net worth in a given time period.</summary>
    public IEnumerable<decimal>? NetWorthProjection { get; private init; }

    /// <summary>List of chronological payments processed from the transaction list.</summary>
    List<Payment> _payments;

    /// <summary>A period filter for what payments should be included in the end projection after calculations.</summary>
    ProjectionTimePeriod _period;

    public static IResult<ProjectionSimulator, DomainException> Create(IEnumerable<Transaction> transactions, ProjectionTimePeriod period) {
        var earliestTransaction = transactions.Select(t => t.PaymentTimeline.Period.Start).Min();
        return GetPaymentChronology(transactions, period).Then(payments => {
            return Result<ProjectionSimulator, DomainException>.Ok(new ProjectionSimulator(payments, period));
        });
    }

    /// <summary>Returns a timeline of payments in chronological order.</summary>
    static IResult<List<Payment>, DomainException> GetPaymentChronology(IEnumerable<Transaction> transactions, ProjectionTimePeriod period) {
        List<Payment> payments = new();

        // filter out transactions that start too late
        var filteredTransactions = transactions.Where(t => t.PaymentTimeline.Period.Start <= period.End);

        foreach (var transaction in filteredTransactions) {
            var paymentDates = transaction.GetPaymentDates(period.End);

            if (paymentDates.Failure) {
                return Result<List<Payment>, DomainException>.Fail(paymentDates.GetErrors());
            }

            payments.AddRange(paymentDates.Unwrap().Select(pd => new Payment {
                Date = pd,
                Amount = (double)transaction.Amount,
                Asset = transaction.Asset is null ? null : new PaymentAsset {
                    ReturnRate = (double)transaction.Asset.ReturnRate / 100,
                    StandardDeviation = (double)transaction.Asset.StandardDeviation / 100,
                    TaxScheme = transaction.TaxScheme
                },
                TaxScheme = transaction.TaxScheme,
                PreRetirement = transaction.Type != TransactionType.RetirementOnly,
                PostRetirement = transaction.Type != TransactionType.PreRetirementOnly
            }));
        }

        payments.Sort((p1, p2) => p1.Date.CompareTo(p2.Date));
        return Result<List<Payment>, DomainException>.Ok(payments);
    }

    ProjectionSimulator(List<Payment> payments, ProjectionTimePeriod period) {
        _payments = payments;
        _period = period;
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
        double taxFreeBalance = 0; // when we withdraw from our portfolio, only profits are taxed

        Dictionary<PaymentAsset, double> assets = new();
        Dictionary<TaxScheme, double> taxDiscounts = new();

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
            if (payment.Asset is null && payment.Amount > 0 && payment.TaxScheme != TaxScheme.NonTaxable) {
                int age = CalculateAge(birthday, date);
                decimal taxRate = CalculateTaxRate(payment.TaxScheme, age);
                liquidBalance -= payment.Amount * (double)(taxRate / 100); // income tax
            }

            // add asset investments
            if (payment.Asset is not null) {
                var asset = (PaymentAsset)payment.Asset;
                assets.TryGetValue(asset, out double amount);
                assets[asset] = amount - payment.Amount;
                assetBalance -= payment.Amount;

                taxFreeBalance -= payment.Amount;
            }

            // in case we are in debt, withdraw from assets to reach zero
            if (liquidBalance < 0) {
                int age = CalculateAge(birthday, date);
                (double withdrawn, double taxed) = WithdrawAssets(assets, -liquidBalance, age, ref taxFreeBalance);
                liquidBalance += withdrawn;
                assetBalance -= withdrawn + taxed;
            }

            // update date and add time point to series
            if (yearsPassed > 0) {
                date = payment.Date;
                if (date >= _period.Start) {
                    series.Add(new TimePoint(date, Math.Ceiling(liquidBalance * 100) / 100, Math.Ceiling(assetBalance * 100) / 100));
                }
            } else {
                if (date >= _period.Start) {
                    series[^1] = new TimePoint(date, Math.Ceiling(liquidBalance * 100) / 100, Math.Ceiling(assetBalance * 100) / 100);
                }
            }
        }

        return Projection.Create(series, retiringAt);
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
    /// Withdraws assets, prioritizing the withdrawal of higher-risk assets, and deprioritizing the withdrawal of assets with unfulfilled tax incentive requirements.
    /// </summary>
    /// <param name="assets">Dictionary of assets and their values.</param>
    /// <param name="amount">Amount to withdraw if possible.</param>
    /// <returns>Amount withdrawn.</returns>
    (double withdrawn, double taxed) WithdrawAssets(Dictionary<PaymentAsset, double> assets, double withdrawalAmount, int age, ref double taxFreeBalance) {
        double withdrawn = 0;
        double taxed = 0;

        foreach ((PaymentAsset asset, double assetAmount) in assets.OrderBy(a => GetTaxSchemePriority(a.Key.TaxScheme, age))
                                                              .ThenBy(a => -a.Key.StandardDeviation)
                                                              .ThenBy(a => a.Key.ReturnRate)) {
            double leftToWithdraw = withdrawalAmount - withdrawn;

            // calculate withdrawal taxes
            decimal taxRate = CalculateTaxRate(asset.TaxScheme, age);
            double taxAmount = Math.Max(leftToWithdraw - taxFreeBalance, 0) * (double)(taxRate / 100);
            double postTaxAssetAmount = assetAmount - taxAmount;

            // withdraw
            if (withdrawalAmount < postTaxAssetAmount) {
                withdrawn += withdrawalAmount;
                assets[asset] -= withdrawalAmount + taxAmount;
                taxFreeBalance = Math.Max(taxFreeBalance - withdrawalAmount, 0);
                taxed += taxAmount;
                break;
            } else {
                withdrawn += postTaxAssetAmount;
                taxFreeBalance = Math.Max(taxFreeBalance - assets[asset], 0);
                assets.Remove(asset); // asset has been emptied
                taxed += taxAmount;
            }
        }

        return (withdrawn, taxed);
    }

    /// <summary>Calculates the tax rate for a tax scheme.</summary>
    decimal CalculateTaxRate(TaxScheme taxScheme, int age) {
        decimal taxRate = taxScheme.TaxRate;

        if (taxRate == 0) {
            return 0;
        }

        if (taxScheme.IncentiveScheme is not null) {
            if (age >= taxScheme.IncentiveScheme.MinAge) {
                taxRate -= taxScheme.IncentiveScheme.TaxRefundRate;
            }
        }

        return taxRate;
    }

    /// <summary>Calculates the priority of assets to withdraw based on whether their tax requirements have been fulfilled.</summary>
    int GetTaxSchemePriority(TaxScheme taxScheme, int age) {
        if (taxScheme.IncentiveScheme is not null && taxScheme.IncentiveScheme.MinAge is not null) {
            bool requiredDiscountAgeReached = age >= taxScheme.IncentiveScheme.MinAge;
            return requiredDiscountAgeReached ? 0 : 1;
        }

        return 0;
    }

    /// <summary>Calculate age, taking into account leap years. Used primarily for tax calculations.</summary>
    int CalculateAge(DateOnly birthday, DateOnly now) {
        var age = now.Year - birthday.Year;
        return birthday > now.AddYears(-age) ? age - 1 : age;
    }
}
