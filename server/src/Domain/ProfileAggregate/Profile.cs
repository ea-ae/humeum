using Domain.Common;
using Domain.TransactionAggregate;

namespace Domain.ProfileAggregate;

/// <summary>
/// Profiles are created by users to store their transactions, custom asset & taxation types,
/// transaction categories, inflation & withdrawal rates, and so on.
/// </summary>
public class Profile : TimestampedEntity {
    public int UserId { get; private set; }

    ICollection<Transaction> _transactions = null!;
    public IEnumerable<Transaction> Transactions => _transactions;
}
