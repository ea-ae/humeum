using Domain.Common;
using Domain.TransactionAggregate;

namespace Domain.ProfileAggregate;

/// <summary>
/// Profiles are created by users to store their transactions, custom asset & taxation types,
/// transaction categories, inflation & withdrawal rates, and so on.
/// </summary>
public class Profile : TimestampedEntity {
    public int UserId { get; private set; }

    public string Name { get; private set; }

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

    public Profile(string name, decimal withdrawalRate, string? description = null) {
        Name = name;
        Description = description;
        WithdrawalRate = withdrawalRate;
    }
}
