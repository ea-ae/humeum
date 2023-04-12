using Domain.AssetAggregate;
using Domain.Common.Exceptions;
using Domain.Common.Interfaces;
using Domain.Common.Models;
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
public class Transaction : TimestampedEntity, IRequiredProfileEntity {
    string? _name;
    public string? Name => _name;

    string? _description;
    public string? Description => _description;

    decimal _amount;
    public decimal Amount => _amount;

    Timeline _paymentTimeline = null!;
    public Timeline PaymentTimeline => _paymentTimeline;

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

    //public Transaction(string? name,
    //                   string? description,
    //                   decimal amount,
    //                   TransactionType type,
    //                   Timeline paymentTimeline,
    //                   Profile profile,
    //                   TaxScheme taxScheme,
    //                   Asset? asset = null)
    //    : this(name, description, amount, type, paymentTimeline, profile.Id, taxScheme.Id, asset?.Id) {
    //    Profile = profile;
    //    TaxScheme = taxScheme;
    //    Asset = asset;
    //}

    //public Transaction(string? name,
    //                   string? description,
    //                   decimal amount,
    //                   TransactionType type,
    //                   Timeline paymentTimeline,
    //                   int profileId,
    //                   int taxSchemeId,
    //                   int? assetId = null) {
    //    Name = name;
    //    Description = description;
    //    TypeId = type.Id;
    //    Type = type;
    //    PaymentTimeline = paymentTimeline;
    //    ProfileId = profileId;
    //    TaxSchemeId = taxSchemeId;
    //    AssetId = assetId;
    //    Amount = amount;
    //}

    Transaction() { }

    public static IResult<Transaction, DomainException> Create(string? name,
                                                               string? description,
                                                               decimal amount,
                                                               TransactionType type,
                                                               Timeline paymentTimeline,
                                                               Profile profile,
                                                               TaxScheme taxScheme,
                                                               Asset? asset = null) {
        var transaction = new Transaction() {
            Profile = profile, ProfileId = profile.Id, TaxScheme = taxScheme, TaxSchemeId = taxScheme.Id, Asset = asset, AssetId = asset?.Id
        };
        var builder = new Result<Transaction, DomainException>.Builder().AddValue(transaction);

        return SetFields(builder, name, description, amount, type, paymentTimeline).Build();
    }

    public static IResult<Transaction, DomainException> Create(string? name,
                                                               string? description,
                                                               decimal amount,
                                                               TransactionType type,
                                                               Timeline paymentTimeline,
                                                               int profileId,
                                                               int taxSchemeId,
                                                               int? assetId = null) {
        var transaction = new Transaction() { ProfileId = profileId, TaxSchemeId = taxSchemeId, AssetId = assetId };
        var builder = new Result<Transaction, DomainException>.Builder().AddValue(transaction);

        return SetFields(builder, name, description, amount, type, paymentTimeline).Build();
    }

    static Result<Transaction, DomainException>.Builder SetFields(Result<Transaction, DomainException>.Builder builder,
                                                                  string? name,
                                                                  string? description,
                                                                  decimal amount,
                                                                  TransactionType type,
                                                                  Timeline paymentTimeline) {
        return builder.Transform(transaction => transaction.SetName(name))
                      .Transform(transaction => transaction.SetDescription(description))
                      .Transform(transaction => transaction.SetAmount(amount))
                      .Transform(transaction => transaction.SetPaymentTimeline(paymentTimeline));
    }



    /// <summary>Replace the transaction fields in-place.</summary>
    public void Replace(string? name, string? description, decimal amount, TransactionType type, Timeline paymentTimeline, TaxScheme taxScheme, Asset? asset) {
        Type = type;
        TypeId = type.Id;
        TaxScheme = taxScheme;
        TaxSchemeId = taxScheme.Id;
        Asset = asset;
        AssetId = asset?.Id;
        SetName(name);
        SetDescription(description);
        SetAmount(amount);
        SetPaymentTimeline(paymentTimeline);
    }

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

    /// <summary>
    /// Total count of payments made in a recurring transaction.
    /// </summary>
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

    /// <summary>
    /// Total amount of payments made throughout a recurring transaction.
    /// </summary>
    public decimal TotalTransactionAmount => Amount * TotalTransactionCount;

    IResult<None, DomainException> SetName(string? name) {
        if (name is null || name == "") {
            _name = null;
        } else if (name.Length > 50) {
            return Result<None, DomainException>.Fail(new DomainException("Name cannot exceed 50 characters."));
        } else {
            _name = name;
        }

        return Result<None, DomainException>.Ok(None.Value);
    }

    IResult<None, DomainException> SetDescription(string? description) {
        if (description is null || description == "") {
            _description = null;
        } else if (description.Length > 400) {
            return Result<None, DomainException>.Fail(new DomainException("Description cannot exceed 400 characters."));
        } else {
            _description = description;
        }

        return Result<None, DomainException>.Ok(None.Value);
    }

    IResult<None, DomainException> SetAmount(decimal amount) {
        if (amount == 0) {
            return Result<None, DomainException>.Fail(new DomainException("Transaction amount cannot be zero."));
        } else if (Asset is not null && amount >= 0) {
            return Result<None, DomainException>.Fail(new DomainException("Asset transactions can only be expenses (a negative amount)."));
        }

        _amount = amount;
        return Result<None, DomainException>.Ok(None.Value);
    }

    IResult<None, DomainException> SetPaymentTimeline(Timeline paymentTimeline) {
        _paymentTimeline = paymentTimeline;
        return Result<None, DomainException>.Ok(None.Value);
    }
}
