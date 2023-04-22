using Domain.Common.Exceptions;
using Domain.Common.Models;
using Domain.TaxSchemeAggregate.ValueObjects;
using Domain.TransactionAggregate;

using Shared.Interfaces;
using Shared.Models;

namespace Domain.TaxSchemeAggregate;

/// <summary>
/// Tax schemes are applied to both income and optionally asset purchase expense transactions.
/// In case of income transactions, the tax rate will be applied onto all expenses made off it.
/// In case of special tax schemes used for asset purchases, the income tax scheme will be overriden
/// for said asset purchase.
/// </summary>
public class TaxScheme : TimestampedEntity {
    public string Name { get; private set; } = null!;

    public string? Description { get; private set; }

    public decimal TaxRate { get; private set; }

    public TaxIncentiveScheme? IncentiveScheme { get; private set; }

    HashSet<Transaction> _transactions = null!;
    public IReadOnlyCollection<Transaction> Transactions => _transactions;

    public TaxScheme(int taxSchemeId, string name, string description, decimal taxRate, TaxIncentiveScheme incentiveScheme)
        : this(name, description, taxRate, incentiveScheme) {
        Id = taxSchemeId;
    }

    public TaxScheme(string name, string description, decimal taxRate, TaxIncentiveScheme incentiveScheme)
        : this(name, description, taxRate) {
        IncentiveScheme = incentiveScheme;
    }

    public TaxScheme(int taxSchemeId, string name, string description, decimal taxRate) : this(name, description, taxRate) {
        Id = taxSchemeId;
    }

    public TaxScheme(string name, string description, decimal taxRate) {
        Name = name;
        Description = description;
        TaxRate = taxRate;
    }

    public static Result<TaxScheme, IBaseException> Create(int taxSchemeId, string name, string description, decimal taxRate,
                                                           TaxIncentiveScheme? incentiveScheme = null) {
        var taxScheme = new TaxScheme() { Id = taxSchemeId, Name = name, Description = description, TaxRate = taxRate };
        var builder = new Result<TaxScheme, IBaseException>.Builder().AddValue(taxScheme);
        return SetFields(builder, taxRate, incentiveScheme).Build();
    }

    public static Result<TaxScheme, IBaseException> Create(string name, string description, decimal taxRate, TaxIncentiveScheme? incentiveScheme = null) {
        var taxScheme = new TaxScheme() { Name = name, Description = description, TaxRate = taxRate };
        var builder = new Result<TaxScheme, IBaseException>.Builder().AddValue(taxScheme);
        return SetFields(builder, taxRate, incentiveScheme).Build();
    }

    static Result<TaxScheme, IBaseException>.Builder SetFields(Result<TaxScheme, IBaseException>.Builder builder,
                                                               decimal taxRate, TaxIncentiveScheme? incentiveScheme) {
        return builder.Transform(taxScheme => taxScheme.SetTaxRate(taxRate))
                      .Transform(taxScheme => taxScheme.SetIncentiveScheme(incentiveScheme));
    }
    
    TaxScheme() { }

    IResult<None, DomainException> SetTaxRate(decimal taxRate) {
        if (taxRate < 0 || taxRate > 100) {
            return Result<None, DomainException>.Fail(new DomainException("Invalid tax rate."));
        }
        TaxRate = taxRate;

        return Result<None, DomainException>.Ok(None.Value);
    }

    IResult<None, DomainException> SetIncentiveScheme(TaxIncentiveScheme? incentiveScheme) {
        if (incentiveScheme is not null && incentiveScheme.TaxRefundRate > TaxRate) {
            return Result<None, DomainException>.Fail(new DomainException("Tax refund rate cannot be higher than tax rate."));
        }
        IncentiveScheme = incentiveScheme;

        return Result<None, DomainException>.Ok(None.Value);
    }

}
