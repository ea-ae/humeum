using Xunit;

using Application.Test.Common;
using AutoMapper;
using Application.Common.Mappings;
using Application.Common.Exceptions;
using Application.Transactions.Queries.GetTransactions;
using Application.Profiles.Commands.AddProfile;
using Application.Profiles.Queries;

namespace Application.Test;

[Collection(InMemoryDbContextCollection.COLLECTION_NAME)]
public class ProfilesTests {
    readonly InMemorySqliteDbContextFixture _dbContextFixture;

    public ProfilesTests(InMemorySqliteDbContextFixture fixture) {
        _dbContextFixture = fixture;
    }

    [Fact]
    public async Task AddProfileCommand_ValidProfile_CreatesProfile() {
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
}
