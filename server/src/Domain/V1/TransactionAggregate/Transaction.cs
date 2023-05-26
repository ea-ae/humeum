using Domain.Common.Exceptions;
using Domain.Common.Interfaces;
using Domain.Common.Models;
using Domain.V1.AssetAggregate;
using Domain.V1.ProfileAggregate;
using Domain.V1.TaxSchemeAggregate;
using Domain.V1.TransactionAggregate.ValueObjects;
using Domain.V1.TransactionCategoryAggregate;

using Shared.Interfaces;
using Shared.Models;

namespace Domain.V1.TransactionAggregate;

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

    public static IResult<Transaction, DomainException> Create(string? name,
                                                               string? description,
                                                               decimal amount,
                                                               TransactionType type,
                                                               Timeline paymentTimeline,
                                                               Profile profile,
                                                               TaxScheme taxScheme,
                                                               Asset? asset = null) {
        var transaction = new Transaction() {
            Type = type,
            TypeId = type.Id,
            Profile = profile,
            ProfileId = profile.Id,
            TaxScheme = taxScheme,
            TaxSchemeId = taxScheme.Id,
            Asset = asset,
            AssetId = asset?.Id
        };
        var builder = new Result<Transaction, DomainException>.Builder().AddValue(transaction);

        return SetFields(builder, name, description, amount, paymentTimeline).Build();
    }

    public static IResult<Transaction, DomainException> Create(string? name,
                                                               string? description,
                                                               decimal amount,
                                                               TransactionType type,
                                                               Timeline paymentTimeline,
                                                               int profileId,
                                                               int taxSchemeId,
                                                               int? assetId = null) {
        var transaction = new Transaction() { Type = type, TypeId = type.Id, ProfileId = profileId, TaxSchemeId = taxSchemeId, AssetId = assetId };
        var builder = new Result<Transaction, DomainException>.Builder().AddValue(transaction);

        return SetFields(builder, name, description, amount, paymentTimeline).Build();
    }

    static Result<Transaction, DomainException>.Builder SetFields(Result<Transaction, DomainException>.Builder builder,
                                                                  string? name,
                                                                  string? description,
                                                                  decimal amount,
                                                                  Timeline paymentTimeline) {
        return builder.Transform(transaction => transaction.SetName(name))
                      .Transform(transaction => transaction.SetDescription(description))
                      .Transform(transaction => transaction.SetAmount(amount))
                      .Transform(transaction => transaction.SetPaymentTimeline(paymentTimeline));
    }

    Transaction() { }

    /// <summary>Replace the transaction fields in-place.</summary>
    public IResult<None, DomainException> Replace(string? name,
                                                  string? description,
                                                  decimal amount,
                                                  TransactionType type,
                                                  Timeline paymentTimeline,
                                                  int taxSchemeId,
                                                  int? assetId) {
        Type = type;
        TypeId = type.Id;
        TaxSchemeId = taxSchemeId;
        AssetId = assetId;

        var builder = new Result<Transaction, DomainException>.Builder().AddValue(this);
        return SetFields(builder, name, description, amount, paymentTimeline).Build().Then(None.Value);
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

    /// <summary>Returns all the dates at which payments are made at for this transaction within a provided time range.</summary>
    public IResult<IEnumerable<DateOnly>, DomainException> GetPaymentDates(DateOnly? until = null) {
        return PaymentTimeline.GetPaymentDates(until);
    }

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
        } else if (AssetId is not null && amount >= 0) {
            return Result<None, DomainException>.Fail(new DomainException("Asset transactions can only be expenses (a negative amount)."));
        } else if (amount <= 0 && AssetId is null && TaxSchemeId != TaxScheme.NonTaxable.Id) {
            return Result<None, DomainException>.Fail(new DomainException("Non-asset expense transactions cannot have taxes. Use TaxScheme.NonTaxable instead."));
        }

        _amount = amount;
        return Result<None, DomainException>.Ok(None.Value);
    }

    IResult<None, DomainException> SetPaymentTimeline(Timeline paymentTimeline) {
        _paymentTimeline = paymentTimeline;
        return Result<None, DomainException>.Ok(None.Value);
    }
}
