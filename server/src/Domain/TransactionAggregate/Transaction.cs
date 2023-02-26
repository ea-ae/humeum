using Domain.AssetAggregate;
using Domain.Common;
using Domain.Common.Exceptions;
using Domain.ProfileAggregate;
using Domain.TaxSchemeAggregate;
using Domain.TransactionAggregate.ValueObjects;
using Domain.TransactionCategoryAggregate;

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

    decimal _amount;
    public decimal Amount {
        get => _amount;
        private set {
            if (value == 0) {
                throw new DomainException(new ArgumentOutOfRangeException(nameof(Amount), "Transaction amount cannot be zero."));
            } else if (Asset is not null && value >= 0) {
                throw new DomainException(new InvalidOperationException("Asset transactions can only be expenses (negative amount)."));
            }
            _amount = value;
        }
    }

    public Timeline PaymentTimeline { get; private set; } = null!;

    public int TypeId { get; private set; }
    public TransactionType Type { get; private set; } = null!;

    public int ProfileId { get; private set; }
    public Profile Profile { get; private set; } = null!;

    public int TaxSchemeId { get; private set; }
    public TaxScheme TaxScheme { get; private set; } = null!;

    public int? AssetId { get; private set; }
    public Asset? Asset { get; private set; }

    HashSet<TransactionCategory> _categories = null!;
    public IReadOnlyCollection<TransactionCategory> Categories => _categories;

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
        PaymentTimeline = paymentTimeline;
        TypeId = type.Id;
        Type = type;
        ProfileId = profileId;
        TaxSchemeId = taxSchemeId;
        AssetId = assetId;
        Amount = amount;
    }

    private Transaction() { }

    /// <summary>
    /// Assigns a category to the transaction if it wasn't already added before.
    /// </summary>
    /// <param name="category">Category to assign.</param>
    /// <returns>Whether the category was assigned (in other words, it didn't exist before).</returns>
    public bool AddCategory(TransactionCategory category) {
        return _categories.Add(category);
    }

    /// <summary>
    /// Removes a category from a transaction in case it was previously assigned.
    /// </summary>
    /// <param name="category">Category to remove.</param>
    /// <returns>Whether the category was removed (in other words, whether it was previously assigned).</returns>
    public bool RemoveCategory(TransactionCategory category) {
        return _categories.Remove(category);
    }

    public int TotalTransactionCount {
        get {
            if (!PaymentTimeline.Period.IsRecurring || PaymentTimeline.Frequency is null) {
                return 1;
            }

            int count = PaymentTimeline.Frequency.TimeUnit.InTimeSpan(PaymentTimeline.Period.Start,
                                                                      (DateOnly)PaymentTimeline.Period.End!);
            return count;
        }
    }

    public decimal TotalTransactionAmount => Amount * TotalTransactionCount;
}
