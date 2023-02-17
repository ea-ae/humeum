﻿using System.Net;

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

    [Theory]
    [InlineData("amount=5&type=RETIREMENTONLY")]
    [InlineData("amount=5&paymentStart=2023-01-01")]
    [InlineData("amount=5&paymentStart=2022")]
    [InlineData("amount=5&type=ALWAYS")]
    [InlineData("amount=5&type=ALWAYS&paymentStart=2023-01-01&paymentEnd=2024-01-01")]
    [InlineData("amount=5&type=ALWAYS&paymentStart=2023-01-01&timeUnit=DAYS")]
    [InlineData("amount=5&type=ALWAYS&paymentStart=2023-01-01&timeUnit=DAYS&timesPerCycle=1&unitsInCycle=1")]
    public async Task AddAction_MissingQueryData_ReturnsBadRequest(string query) {
        string url = $"users/1/profiles/1/transactions?{query}";
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
        //await context.Database.EnsureDeletedAsync();
        await context.Database.EnsureCreatedAsync();

        // create 2 profiles

        var response = await client.PostAsync("users/1/profiles?name=Default", null);
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.NotNull(response.Headers.Location);
        int profileOneId = int.Parse(response.Headers.Location.ToString().Split("/").Last());

        response = await client.PostAsync("users/1/profiles?name=Conservative&withdrawalRate=3.15", null);
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.NotNull(response.Headers.Location);
        int profileTwoId = int.Parse(response.Headers.Location.ToString().Split("/").Last());

        // create 3 transactions per profile

        const int transactionsPerProfile = 3;
        foreach (int profileId in new[] { profileOneId, profileTwoId }) {
            for (int i = 0; i < transactionsPerProfile; i++) {
                response = await client.PostAsync($"users/1/profiles/{profileId}/transactions?{transactionQuery}", null);
                Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            }
        }

        // soft delete one of the profiles

        response = await client.DeleteAsync("users/1/profiles/2");
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

        // confirm that soft deletion was applied correctly

        bool profileOneDeleted = context.Profiles.Any(p => p.Id == profileOneId && p.DeletedAt != null);
        bool profileTwoDeleted = context.Profiles.Any(p => p.Id == profileTwoId && p.DeletedAt != null);

        Assert.False(profileOneDeleted);
        Assert.True(profileTwoDeleted);

        int profileOneTransactionsDeleted = context.Transactions.AsNoTracking()
                                                                .Where(t => t.ProfileId == profileOneId && t.DeletedAt != null)
                                                                .Count();
        int profileTwoTransactionsDeleted = context.Transactions.AsNoTracking()
                                                                .Where(t => t.ProfileId == profileTwoId && t.DeletedAt != null)
                                                                .Count();

        Assert.Equal(0, profileOneTransactionsDeleted);
        Assert.Equal(transactionsPerProfile, profileTwoTransactionsDeleted);
    }
}