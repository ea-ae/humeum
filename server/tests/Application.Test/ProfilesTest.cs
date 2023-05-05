using Application.Common.Extensions;
using Application.Test.Common;
using Application.Test.Common.Stubs;
using Application.V1.Profiles.Commands;
using Domain.V1.AssetAggregate;
using Domain.V1.AssetAggregate.ValueObjects;
using Domain.V1.ProfileAggregate;
using Domain.V1.TransactionAggregate;
using Domain.V1.TransactionAggregate.ValueObjects;
using Xunit;

namespace Application.Test;

/// <summary>
/// Tests centered generally around profile services.
/// </summary>
[Collection(InMemoryDbContextCollection.COLLECTION_NAME)]
public class ProfilesTests {
    readonly InMemorySqliteDbContextFixture _dbContextFixture;

    public ProfilesTests(InMemorySqliteDbContextFixture fixture) {
        _dbContextFixture = fixture;
    }

    [Fact]
    public async Task AddProfileCommand_ValidProfile_ReturnsCreatedProfileId() {
        var context = _dbContextFixture.CreateDbContext();
        var handler = new AddProfileCommandHandler(context, new ApplicationUserServiceStub());
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
        int profileId = (await handler.Handle(command)).Unwrap();
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
        var handler = new DeleteProfileCommandHandler(context, new ApplicationUserServiceStub());

        // create a new profile

        var profile = Profile.Create(1000, "My Profile Name", "About to delete", 5.5m).Unwrap();
        context.Profiles.Add(profile);
        await context.SaveChangesAsync();

        // add profile entities that will be cascade-soft-deleted alongside the profile

        var transaction = Transaction.Create("My Transaction",
                                             null,
                                             1,
                                             context.GetEnumerationEntityByCode<TransactionType>("ALWAYS").Unwrap(),
                                             Timeline.Create(TimePeriod.Create(new DateOnly(2021, 1, 1)).Unwrap()).Unwrap(),
                                             profile.Id,
                                             taxSchemeId).Unwrap();
        context.AssetTypes.Attach(AssetType.RealEstate);
        var asset = Asset.Create("My Asset", "About to delete", 7.9m, 11, AssetType.RealEstate, profile).Unwrap();

        context.Transactions.Add(transaction);
        context.Assets.Add(asset);
        await context.SaveChangesAsync();

        // assert that nothing is deleted yet

        Assert.Null(profile.DeletedAt);
        Assert.Null(profile.DeletedAt);
        Assert.Null(asset.DeletedAt);

        // delete profile (as well as the associated entities)

        DeleteProfileCommand command = new() {
            User = 1000,
            Profile = profile.Id
        };
        await handler.Handle(command);

        // assert that profile and profile entities are deleted

        await context.Profiles.Entry(profile).ReloadAsync();
        await context.Transactions.Entry(transaction).ReloadAsync();
        await context.Assets.Entry(asset).ReloadAsync();

        Assert.NotNull(profile.DeletedAt);
        Assert.NotNull(transaction.DeletedAt);
        Assert.NotNull(asset.DeletedAt);
    }
}
