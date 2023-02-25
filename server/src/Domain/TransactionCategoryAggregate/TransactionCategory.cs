using Domain.Common;
using Domain.Common.Exceptions;
using Domain.ProfileAggregate;
using Domain.TransactionAggregate;

namespace Domain.TransactionCategoryAggregate;

/// <summary>
/// Optional many-to-many categories/tags for transactions. Custom categories can
/// be created by profiles for filtering and/or statistics.
/// </summary>
public class TransactionCategory : TimestampedEntity {
    string _name = null!;
    public string Name {
        get => _name;
        private set {
            if (value.Length == 0) {
                throw new DomainException(new ArgumentException("Name cannot be empty.", nameof(Name)));
            }
            _name = value;
        }
    }

    public int? ProfileId { get; private set; }
    public Profile? Profile { get; private set; }

    HashSet<Transaction> _transactions = null!;
    public IReadOnlyCollection<Transaction> Transactions => _transactions;

    public TransactionCategory(string name, Profile? profile = null) : this(name, profile?.Id) {
        Profile = profile;
    }

    public TransactionCategory(string name, int? profileId = null) {
        Name = name;
        ProfileId = profileId;
    }

    public TransactionCategory(int categoryId, string name) {
        Id = categoryId;
        Name = name;
    }

    private TransactionCategory() { }
}
