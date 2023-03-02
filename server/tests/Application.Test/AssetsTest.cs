using Application.Assets.Commands;
using Application.Test.Common;

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

        AddAssetCommand command = new() {
            User = 1,
            Profile = profile.Id,
            Name = "Custom asset",
            Description = "My asset description",
            ReturnRate = 5.1m,
            StandardDeviation = 11.5m,
            AssetType = AssetType.RealEstate.Code
        };

        // create asset

        var assetId = await handler.Handle(command);
        var asset = context.Assets.First(a => a.Id == assetId);

        // confirm details

        Assert.Equal(command.Profile, asset.ProfileId);
        Assert.Equal(command.Name, asset.Name);
        Assert.Equal(command.Description, asset.Description);
        Assert.Equal(command.ReturnRate, asset.ReturnRate);
        Assert.Equal(command.StandardDeviation, asset.StandardDeviation);
        Assert.Equal(command.AssetType, asset.Type.Code);
    }
}
