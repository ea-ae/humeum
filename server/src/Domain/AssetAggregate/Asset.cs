using Domain.Common;
using Domain.Common.Exceptions;
using Domain.Common.Interfaces;
using Domain.ProfileAggregate;
using Domain.TransactionAggregate;
using Domain.TransactionAggregate.ValueObjects;

namespace Domain.AssetAggregate;

/// <summary>
/// Assets are the available investment options, such as an S&P500 index fund or US treasury bonds.
/// Every transaction can optionally be associated with an asset, which declares it as an investment
/// expense (asset income is not a possibility, because profit investment is implicit for now).
/// Assets have an average return rate (growth) and a standard deviation value (volatility).
/// Assets can be created by users or be provided as non-deletable defaults by the application.
/// In case of the latter, the asset types may utilize more complex forms of calculation such as
/// referencing historical market rate return distributions. Assets are withdrawn in order of returns.
/// </summary>
public class Asset : TimestampedEntity, IOptionalProfileEntity {
    public string Name { get; private set; } = null!;

    public string? Description { get; private set; }

    decimal _returnRate;
    public decimal ReturnRate {
        get => _returnRate;
        set {
            if (value < 0) {
                throw new DomainException(new ArgumentException("Return rate cannot be negative."));
            }
            _returnRate = value;
        }
    }

    decimal _standardDeviation;
    public decimal StandardDeviation {
        get => _standardDeviation;
        set {
            if (value < 0) {
                throw new DomainException(new ArgumentException("Standard deviation cannot be negative."));
            }
            _standardDeviation = value;
        }
    }

    public int TypeId { get; private set; }
    public AssetType Type { get; private set; } = null!;

    HashSet<Transaction> _transactions = null!;
    public IReadOnlyCollection<Transaction> Transactions => _transactions;

    public int? ProfileId { get; private set; }
    public Profile? Profile { get; private set; }

    public Asset(string name, string? description, decimal returnRate, decimal standardDeviation, AssetType type, Profile profile)
        : this(name, description, returnRate, standardDeviation, type.Id, profile.Id) {
        Type = type;
        Profile = profile;
    }

    public Asset(string name, string? description, decimal returnRate, decimal standardDeviation, int typeId, int profileId)
        : this(name, description, returnRate, standardDeviation, typeId) {
        ProfileId = profileId;
    }

    public Asset(int assetId, string name, string? description, decimal returnRate, decimal standardDeviation, int typeId)
        : this(name, description, returnRate, standardDeviation, typeId) {
        Id = assetId;
    }

    public Asset(string name, string? description, decimal returnRate, decimal standardDeviation, int typeId) {
        Name = name;
        Description = description;
        ReturnRate = returnRate;
        StandardDeviation = standardDeviation;
        TypeId = typeId;
    }

    private Asset() { }

    public bool IsDefaultAsset => ProfileId is not null || Profile is not null;
}
