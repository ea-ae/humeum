using Domain.Common.Exceptions;
using Domain.Common.Models;

using Shared.Interfaces;
using Shared.Models;

namespace Domain.TaxSchemeAggregate.ValueObjects;

/// <summary>
/// The tax discount rate and conditions for receiving tax incentive rewards.
/// </summary>
public class TaxIncentiveScheme : ValueObject {
    /// <summary>Income tax refund rate for investments. E.g. for a 20% tax rate, a 10% discount would mean a 50% return.</summary>
    public decimal? TaxRefundRate { get; private init; }

    /// <summary>The optional minimum age requirement at which the tax incentive benefits can be collected.</summary>
    public int? MinAge { get; private init; }

    /// <summary>
    /// The optional maximum annual income percentage that can be applied onto this tax incentive scheme. In the case there is
    /// also a maximum annual income sum, whichever is lower is set as the limit.
    /// </summary>
    public decimal? MaxIncomePercentage { get; private init; }

    /// <summary>
    /// The optional maximum annual income sum that can be applied onto this tax incentive scheme. In case there is
    /// also a maximum annual income percentage, whichever is lower is set as the limit.
    /// </summary>
    public int? MaxApplicableIncome { get; private init; }

    /// <summary>Create a tax incentive scheme. Default values that imply no incentive property (e.g. minimum age of 0) are converted to nulls.</summary>
    public static IResult<TaxIncentiveScheme, DomainException> Create(decimal taxRefundRate, int? minAge,
                                                                      decimal? maxIncomePercentage,
                                                                      int? maxApplicableIncome) {
        var builder = new Result<TaxIncentiveScheme, DomainException>.Builder();
        
        if (taxRefundRate < 0 || taxRefundRate > 100) {
            builder.AddError(new DomainException(new ArgumentOutOfRangeException(nameof(TaxRefundRate), "Invalid tax refund rate percentage.")));
        }

        if (minAge < 0) {
            builder.AddError(new DomainException(new ArgumentOutOfRangeException(nameof(MinAge), "Age cannot be negative.")));
        } else if (minAge == 0) {
            minAge = null;
        }

        if (maxIncomePercentage < 0 || maxIncomePercentage > 100) {
            builder.AddError(new DomainException(new ArgumentOutOfRangeException(nameof(MaxIncomePercentage), "Invalid income percentage.")));
        } else if (maxIncomePercentage == 100) {
            maxIncomePercentage = null;
        }

        if (maxApplicableIncome < 0) {
            var error = new DomainException(new ArgumentOutOfRangeException(nameof(MaxApplicableIncome), "Maximum applicable income is below zero."));
            builder.AddError(error);
        }

        var taxIncentiveScheme = new TaxIncentiveScheme() { 
            TaxRefundRate = taxRefundRate, MinAge = minAge, MaxIncomePercentage = maxIncomePercentage, MaxApplicableIncome = maxApplicableIncome };
        return builder.AddValue(taxIncentiveScheme).Build();
    }

    TaxIncentiveScheme() { }

    protected override IEnumerable<object?> GetEqualityComponents() {
        yield return TaxRefundRate;
        yield return MinAge;
        yield return MaxIncomePercentage;
        yield return MaxApplicableIncome;
    }
}
