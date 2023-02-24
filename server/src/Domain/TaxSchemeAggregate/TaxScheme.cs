using Domain.Common;
using Domain.Common.Exceptions;
using Domain.TaxSchemeAggregate.ValueObjects;
using Domain.TransactionAggregate;

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

    decimal _taxRate;
    public decimal TaxRate {
        get => _taxRate;
        private set {
            if (value < 0 && value > 100) {
                throw new DomainException(new ArgumentOutOfRangeException(nameof(value), "Invalid tax rate percentage value."));
            }
            _taxRate = value;
        }
    }

    TaxIncentiveScheme? _incentiveScheme;
    public TaxIncentiveScheme? IncentiveScheme {
        get => _incentiveScheme;
        private set {
            if (value is not null && value.TaxRefundRate > TaxRate) {
                throw new DomainException(new InvalidOperationException("Tax refund rate cannot be higher than tax rate."));
            }
            _incentiveScheme = value;
        }
    }

    HashSet<Transaction> _transactions = null!;
    public IReadOnlyCollection<Transaction> Transactions => _transactions;

    public TaxScheme(string name, string description, decimal taxRate, TaxIncentiveScheme incentiveScheme)
        : this(name, description, taxRate) {
        IncentiveScheme = incentiveScheme;
    }

    public TaxScheme(string name, string description, decimal taxRate) {
        Name = name;
        Description = description;
        TaxRate = taxRate;
    }

    private TaxScheme() { }
}
