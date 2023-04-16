using Domain.Common.Exceptions;
using Domain.Common.Interfaces;
using Domain.Common.Models;
using Domain.ProfileAggregate;
using Domain.TransactionAggregate;
using Domain.TransactionAggregate.ValueObjects;

using Shared.Interfaces;
using Shared.Models;

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
    string _name = null!;
    public string Name => _name;

    string? _description;
    public string? Description => _description;

    decimal _returnRate;
    public decimal ReturnRate => _returnRate;

    decimal _standardDeviation;
    public decimal StandardDeviation => _standardDeviation;

    public int TypeId { get; private set; }
    public AssetType Type { get; private set; } = null!;

    HashSet<Transaction> _transactions = null!;
    public IReadOnlyCollection<Transaction> Transactions => _transactions;

    public int? ProfileId { get; private set; }
    public Profile? Profile { get; private set; }

    Asset() { }

    public static IResult<Asset, DomainException> Create(string name,
                                                         string? description,
                                                         decimal returnRate,
                                                         decimal standardDeviation,
                                                         AssetType type,
                                                         Profile profile) {
        var asset = new Asset() { Type = type, TypeId = type.Id, Profile = profile, ProfileId = profile.Id };
        var builder = new Result<Asset, DomainException>.Builder().AddValue(asset);

        return SetFields(builder, name, description, returnRate, standardDeviation).Build();
    }

    public static IResult<Asset, DomainException> Create(string name,
                                                         string? description,
                                                         decimal returnRate,
                                                         decimal standardDeviation,
                                                         int typeId,
                                                         int profileId) {
        var asset = new Asset() { TypeId = typeId, ProfileId = profileId };
        var builder = new Result<Asset, DomainException>.Builder().AddValue(asset);

        return SetFields(builder, name, description, returnRate, standardDeviation).Build();
    }

    public static IResult<Asset, DomainException> Create(int assetId,
                                                         string name,
                                                         string? description,
                                                         decimal returnRate,
                                                         decimal standardDeviation,
                                                         int typeId) {
        var asset = new Asset() { Id = assetId, TypeId = typeId };
        var builder = new Result<Asset, DomainException>.Builder().AddValue(asset);

        return SetFields(builder, name, description, returnRate, standardDeviation).Build();
    }

    static Result<Asset, DomainException>.Builder SetFields(Result<Asset, DomainException>.Builder builder,
                                                            string name,
                                                            string? description,
                                                            decimal returnRate,
                                                            decimal standardDeviation) {
        return builder.Transform(asset => asset.SetName(name))
                      .Transform(asset => asset.SetDescription(description))
                      .Transform(asset => asset.SetReturnRate(returnRate))
                      .Transform(asset => asset.SetStandardDeviation(standardDeviation));
    }

    public bool IsDefaultAsset => ProfileId is not null || Profile is not null;

    IResult<None, DomainException> SetName(string name) {
        if (name.Length > 50) {
            return Result<None, DomainException>.Fail(new DomainException("Name cannot exceed 50 characters."));
        }

        _name = name;
        return Result<None, DomainException>.Ok(None.Value);
    }

    IResult<None, DomainException> SetDescription(string? description) {
        if (description is not null && description.Length > 400) {
            return Result<None, DomainException>.Fail(new DomainException("Description cannot exceed 400 characters."));
        } else if (description?.Length == 0) {
            description = null;
        }

        _description = description;
        return Result<None, DomainException>.Ok(None.Value);
    }

    IResult<None, DomainException> SetReturnRate(decimal returnRate) {
        if (returnRate < 0) {
            return Result<None, DomainException>.Fail(new DomainException("Return rate cannot be negative."));
        }

        _returnRate = returnRate;
        return Result<None, DomainException>.Ok(None.Value);
    }

    IResult<None, DomainException> SetStandardDeviation(decimal standardDeviation) {
        if (standardDeviation < 0) {
            return Result<None, DomainException>.Fail(new DomainException("Standard deviation cannot be negative."));
        }

        _standardDeviation = standardDeviation;
        return Result<None, DomainException>.Ok(None.Value);
    }
}
