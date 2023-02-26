using Application.Common.Extensions;
using Application.Profiles.Commands.AddProfile;
using Application.Profiles.Commands.DeleteProfile;
using Application.Test.Common;

using Domain.AssetAggregate;
using Domain.ProfileAggregate;
using Domain.TransactionAggregate;
using Domain.TransactionAggregate.ValueObjects;

using Xunit;

namespace Application.Test;

[Collection(InMemoryDbContextCollection.COLLECTION_NAME)]
public class ProfilesTests {
    readonly InMemorySqliteDbContextFixture _dbContextFixture;

    public ProfilesTests(InMemorySqliteDbContextFixture fixture) {
        _dbContextFixture = fixture;
    }

    [Fact]
    public async Task AddProfileCommand_ValidProfile_ReturnsCreatedProfileId() {
        var context = _dbContextFixture.CreateDbContext();
        var handler = new AddProfileCommandHandler(context);
        int userId = 5;
        string profileName = "Test";
        string profileDescription = "Desc";
        decimal profileWithdrawalRate = 1.5m;

        AddProfileCommand command = new() {
            User = userId,
            Name = profileName,
            Description = profileDescription,
            WithdrawalRate = profileWithdrawalRate
        };
        var profileId = await handler.Handle(command);
        var profile = context.Profiles.FirstOrDefault(p => p.Id == profileId);

        Assert.NotNull(profile);
        Assert.Equal(userId, profile.UserId);
        Assert.Equal(profileName, profile.Name);
        Assert.Equal(profileDescription, profile.Description);
        Assert.Equal(profileWithdrawalRate, profile.WithdrawalRate);
    }

    [Fact]
    public async Task DeleteProfileCommand_ValidProfileAndChildren_CascadeDeletesProfile() {
        const int taxSchemeId = 1;
        var context = _dbContextFixture.CreateDbContext();
        var handler = new DeleteProfileCommandHandler(context);

        var profile = new Profile(1000, "My Profile Name", "About to delete", 5.5m);
        context.Profiles.Add(profile);
        await context.SaveChangesAsync();

        var transaction = new Transaction("My Transaction",
                                          null,
                                          1,
                                          context.GetEnumerationEntityByCode<TransactionType>("ALWAYS"),
                                          new Timeline(new TimePeriod(new DateOnly(2021, 1, 1))),
                                          profile.Id,
                                          taxSchemeId);
        context.AssetTypes.Attach(AssetType.RealEstate);
        var asset = new Asset("My Asset", "About to delete", 7.9m, 11, AssetType.RealEstate, profile);

        context.Transactions.Add(transaction);
        context.Assets.Add(asset);
        await context.SaveChangesAsync();

        Assert.Null(profile.DeletedAt);
        Assert.Null(profile.DeletedAt);
        Assert.Null(asset.DeletedAt);

        DeleteProfileCommand command = new() {
            User = 1000,
            Profile = profile.Id
        };
        await handler.Handle(command);

        await context.Profiles.Entry(profile).ReloadAsync();
        await context.Transactions.Entry(transaction).ReloadAsync();
        await context.Assets.Entry(asset).ReloadAsync();

        Assert.NotNull(profile.DeletedAt);
        Assert.NotNull(transaction.DeletedAt);
        Assert.NotNull(asset.DeletedAt);
    }
}
