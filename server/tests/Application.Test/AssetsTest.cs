using Application.Assets.Commands;
using Application.Assets.Queries;
using Application.Common.Extensions;
using Application.Common.Mappings;
using Application.Test.Common;

using AutoMapper;

using Domain.AssetAggregate;
using Domain.TransactionAggregate.ValueObjects;

using Xunit;

namespace Application.Test;

/// <summary>
/// Tests centered generally around transaction category services.
/// </summary>
[Collection(InMemoryDbContextCollection.COLLECTION_NAME)]
public class AssetsTest {
    readonly InMemorySqliteDbContextFixture _dbContextFixture;

    public AssetsTest(InMemorySqliteDbContextFixture fixture) {
        _dbContextFixture = fixture;
    }

    [Fact]
    public async Task AddAssetCommand_CustomAsset_Persists() {
        var context = _dbContextFixture.CreateDbContext();
        var handler = new AddAssetCommandHandler(context);

        var profile = new Domain.ProfileAggregate.Profile(1, "Default");
        context.Profiles.Add(profile);
        await context.SaveChangesAsync();

        // create asset

        AddAssetCommand command = new() {
            Profile = profile.Id,
            Name = "Custom asset",
            Description = "My asset description",
            ReturnRate = 5.1m,
            StandardDeviation = 11.5m,
            AssetType = AssetType.RealEstate.Code
        };
        var assetId = await handler.Handle(command);
        var asset = context.Assets.First(a => a.Id == assetId.Value);

        // confirm details

        Assert.Equal(command.Profile, asset.ProfileId);
        Assert.Equal(command.Name, asset.Name);
        Assert.Equal(command.Description, asset.Description);
        Assert.Equal(command.ReturnRate, asset.ReturnRate);
        Assert.Equal(command.StandardDeviation, asset.StandardDeviation);
        Assert.Equal(command.AssetType, asset.Type.Code);
    }

    [Fact]
    public async Task GetAssetQuery_OneAsset_ReturnsDto() {
        var context = _dbContextFixture.CreateDbContext();
        var mapperConfig = new MapperConfiguration(cfg => { cfg.AddProfile(new AppMappingProfile()); });
        var handler = new GetAssetQueryHandler(context, mapperConfig.CreateMapper());

        // create profile and asset

        var profile = new Domain.ProfileAggregate.Profile(1, "Default");
        var assetType = context.GetEnumerationEntityByCode<AssetType>(AssetType.Bond.Code);
        var asset = new Asset("My asset", null, 5m, 5m, assetType, profile);
        context.Profiles.Add(profile);
        context.Assets.Add(asset);
        await context.SaveChangesAsync();

        // get asset DTO

        GetAssetQuery query = new() {
            Profile = profile.Id,
            Asset = asset.Id
        };
        AssetDto assetDto = (await handler.Handle(query)).Value;

        // confirm details

        Assert.Equal(asset.Id, assetDto.Id);
        Assert.Equal(asset.Name, assetDto.Name);
        Assert.Equal(asset.Description, assetDto.Description);
        Assert.Equal(asset.ReturnRate, assetDto.ReturnRate);
        Assert.Equal(asset.StandardDeviation, assetDto.StandardDeviation);
        Assert.Equal(assetType.Id, assetDto.Type.Id);
        Assert.Equal(assetType.Name, assetDto.Type.Name);
    }
}
