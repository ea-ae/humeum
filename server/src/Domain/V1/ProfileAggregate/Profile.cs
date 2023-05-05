using Domain.Common.Exceptions;
using Domain.Common.Models;
using Domain.V1.AssetAggregate;
using Domain.V1.TransactionAggregate;
using Domain.V1.TransactionCategoryAggregate;
using Shared.Interfaces;
using Shared.Models;

namespace Domain.V1.ProfileAggregate;

/// <summary>
/// Profiles are created by users to store their transactions, custom asset & taxation types,
/// transaction categories, inflation & withdrawal rates, and so on.
/// </summary>
public class Profile : TimestampedEntity
{
    public int UserId { get; private set; }

    public string Name { get; private set; } = null!;

    public string? Description { get; private set; }

    /// <summary>
    /// Rate at which we wish to withdraw from the portfolio upon hitting the retirement goal.
    /// The traditional 4% rule works well for traditional 30-year retirement windows (at 65).
    /// A more aggressive 5% is recommended by many not as worried about market crashes.
    /// A conservative 3-3.5% is often recommended for early retirees.
    /// </summary>
    public decimal WithdrawalRate { get; private set; } = 3.5m;

    HashSet<Transaction> _transactions = null!;
    public IReadOnlyCollection<Transaction> Transactions => _transactions;

    HashSet<TransactionCategory> _transactionCategories = null!;
    public IReadOnlyCollection<TransactionCategory> TransactionCategories => _transactionCategories;

    HashSet<Asset> _assets = null!;
    public IReadOnlyCollection<Asset> Assets => _assets;

    public static IResult<Profile, DomainException> Create(int userId, string name, string? description = null, decimal? withdrawalRate = null)
    {
        var builder = new Result<Profile, DomainException>.Builder();

        if (name.Length > 50)
        {
            builder.AddError(new DomainException("Name cannot exceed 50 characters."));
        }

        if (description == "")
        {
            description = null;
        }

        if (withdrawalRate <= 0)
        {
            builder.AddError(new DomainException("Withdrawal rate cannot be zero or below."));
        }

        var profile = new Profile() { UserId = userId, Name = name, Description = description, WithdrawalRate = withdrawalRate ?? 3.5m };

        return builder.AddValue(profile).Build();
    }

    Profile() { }
}
