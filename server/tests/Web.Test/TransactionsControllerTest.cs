using System.Net;

using Microsoft.Extensions.DependencyInjection;

using Web.Test.Common;

using Xunit;
using Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Xunit.Abstractions;

namespace Web.Test;

/// <summary>
/// End-to-end tests for TransactionsController actions and its dependencies.
/// </summary>
[Collection(WebAppFactoryCollection.COLLECTION_NAME)]
public class TransactionsControllerTest {
    readonly CustomWebAppFactory _webapp;

    public TransactionsControllerTest(CustomWebAppFactory webapp) {
        _webapp = webapp;
    }

    [Fact]
    public async Task AddAction_MissingQueryData_ReturnsBadRequest() {
        const string url = "users/1/profiles/1/transactions?amount=5&type=RETIREMENTONLY";
        var client = _webapp.ConfiguredClient;

        var expected = HttpStatusCode.BadRequest;

        var response = await client.PostAsync(url, null);
        var actual = response.StatusCode;

        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task AddAction_CreateProfileAndTransactionsAndDeleteProfile_ReturnsCreated() {
        const string transactionQuery = "amount=5&type=RETIREMENTONLY&paymentStart=2030-06-06";
        var client = _webapp.ConfiguredClient;

        using var scope = _webapp.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<IAppDbContext>();
        await context.Database.EnsureDeletedAsync();
        await context.Database.EnsureCreatedAsync();

        // create 2 profiles

        var response = await client.PostAsync("users/1/profiles?name=Default", null);
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        response = await client.PostAsync("users/1/profiles?name=Conservative&withdrawalRate=3.15", null);
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);

        // create 3 transactions per profile

        const int transactionsPerProfile = 3;
        for (int profileId = 1; profileId <= 2; profileId++) {
            for (int i = 0; i < transactionsPerProfile; i++) {
                response = await client.PostAsync($"users/1/profiles/{profileId}/transactions?{transactionQuery}", null);
                Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            }
        }

        // soft delete one of the profiles

        response = await client.DeleteAsync("users/1/profiles/2");
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

        // confirm that soft deletion was applied correctly

        bool profileOneDeleted = context.Profiles.Any(p => p.Id == 1 && p.DeletedAt != null);
        bool profileTwoDeleted = context.Profiles.Any(p => p.Id == 2 && p.DeletedAt != null);

        Assert.False(profileOneDeleted);
        Assert.True(profileTwoDeleted);

        int profileOneTransactions = context.Transactions.AsNoTracking().Where(t => t.ProfileId == 1 && t.DeletedAt != null).Count();
        int profileTwoTransactions = context.Transactions.AsNoTracking().Where(t => t.ProfileId == 2 && t.DeletedAt != null).Count();

        Assert.Equal(0, profileOneTransactions);
        Assert.Equal(transactionsPerProfile, profileTwoTransactions);
    }
}
