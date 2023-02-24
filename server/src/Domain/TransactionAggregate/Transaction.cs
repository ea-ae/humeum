using Domain.AssetAggregate;
using Domain.Common;
using Domain.ProfileAggregate;
using Domain.TaxSchemeAggregate;
using Domain.TransactionAggregate.ValueObjects;

namespace Domain.TransactionAggregate;

/// <summary>
/// Transactions are the planned payments of a user. They can be either single-time payments,
/// or recurring ones with a certain frequency in a provided time period. Positive amounts signify
/// income (payments coming in), negative amounts signify expenses (payments going out). Transactions
/// can also be conditional on the dynamic time point at which one retires, which is signified through
/// the transaction type.
/// </summary>
public class Transaction : TimestampedEntity {
    public string? Name { get; private set; }

    public string? Description { get; private set; }

    public decimal Amount { get; private set; }

    public Timeline PaymentTimeline { get; private set; } = null!;

    public int TypeId { get; private set; }
    public TransactionType Type { get; private set; } = null!;

    public int ProfileId { get; private set; }
    public Profile Profile { get; private set; } = null!;

    public int TaxSchemeId { get; private set; }
    public TaxScheme TaxScheme { get; private set; } = null!;

    public int? AssetId { get; private set; }
    public Asset? Asset { get; private set; }

    public Transaction(string? name,
                       string? description,
                       decimal amount,
                       TransactionType type,
                       Timeline paymentTimeline,
                       Profile profile,
                       TaxScheme taxScheme,
                       Asset? asset = null)
        : this(name, description, amount, type, paymentTimeline, profile.Id, taxScheme.Id, asset?.Id) {
        Profile = profile;
    }

    public Transaction(string? name,
                       string? description,
                       decimal amount,
                       TransactionType type,
                       Timeline paymentTimeline,
                       int profileId,
                       int taxSchemeId,
                       int? assetId = null) {
        Name = name;
        Description = description;
        Amount = amount;
        PaymentTimeline = paymentTimeline;
        TypeId = type.Id;
        Type = type;
        ProfileId = profileId;
        TaxSchemeId = taxSchemeId;
        AssetId = assetId;
    }

    private Transaction() { }

    public int TotalTransactionCount {
        get {
            if (!PaymentTimeline.Period.IsRecurring || PaymentTimeline.Frequency is null) {
                return 1;
            }

            // todo: change this param/arg to just the type itself
            int count = PaymentTimeline.Frequency.TimeUnit.InTimeSpan(PaymentTimeline.Period.Start,
                                                                  (DateOnly)PaymentTimeline.Period.End!);
            return count;
        }
    }

    public decimal TotalTransactionAmount => Amount * TotalTransactionCount;
}
