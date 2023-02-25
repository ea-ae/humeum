using Domain.Common;
using Domain.Common.Exceptions;

namespace Domain.TaxSchemeAggregate.ValueObjects;

/// <summary>
/// The tax discount rate and conditions for receiving tax incentive rewards.
/// </summary>
public class TaxIncentiveScheme : ValueObject {
    decimal _taxRefundRate;
    /// <summary>Income tax refund rate for investments. E.g. for a 20% tax rate, a 10% discount would mean a 50% return.</summary>
    public decimal TaxRefundRate {
        get => _taxRefundRate;
        private init {
            if (value < 0 || value > 100) {
                throw new DomainException(new ArgumentOutOfRangeException(nameof(TaxRefundRate), "Invalid tax refund rate percentage."));
            }
            _taxRefundRate = value;
        }
    }

    /// <summary>The optional minimum age requirement at which the tax incentive benefits can be collected.</summary>
    int? _minAge;
    public int? MinAge {
        get => _minAge;
        private init {
            if (value < 0) {
                throw new DomainException(new ArgumentOutOfRangeException(nameof(MinAge), "Age cannot be negative."));
            } else if (value == 0) {
                _minAge = null;
            } else {
                _minAge = value;
            }
        }
    }

    decimal? _maxIncomePercentage;
    /// <summary>
    /// The optional maximum annual income percentage that can be applied onto this tax incentive scheme. In the case there is
    /// also a maximum annual income sum, whichever is lower is set as the limit.
    /// </summary>
    public decimal? MaxIncomePercentage {
        get => _maxIncomePercentage;
        private init {
            if (value < 0 || value > 100) {
                throw new DomainException(new ArgumentOutOfRangeException(nameof(MaxIncomePercentage), "Invalid income percentage."));
            } else if (value == 100) {
                _maxIncomePercentage = null;
            } else {
                _maxIncomePercentage = value;
            }
        }
    }

    int? _maxApplicableIncome;
    /// <summary>
    /// The optional maximum annual income sum that can be applied onto this tax incentive scheme. In case there is
    /// also a maximum annual income percentage, whichever is lower is set as the limit.
    /// </summary>
    public int? MaxApplicableIncome {
        get => _maxApplicableIncome;
        private init {
            if (value < 0) {
                throw new DomainException(
                    new ArgumentOutOfRangeException(nameof(MaxApplicableIncome), "Maximum applicable income is below zero."));
            }
            _maxApplicableIncome = value;
        }
    }

    public TaxIncentiveScheme(decimal taxRefundRate, int? minAge, decimal? maxIncomePercentage, int? maxApplicableIncome) {
        TaxRefundRate = taxRefundRate;
        MinAge = minAge;
        MaxIncomePercentage = maxIncomePercentage;
        MaxApplicableIncome = maxApplicableIncome;
    }

    private TaxIncentiveScheme() { }

    protected override IEnumerable<object?> GetEqualityComponents() {
        yield return TaxRefundRate;
        yield return MinAge;
        yield return MaxIncomePercentage;
        yield return MaxApplicableIncome;
    }
}
