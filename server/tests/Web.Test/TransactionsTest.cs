﻿using System.Net;

using Application.Common.Interfaces;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using Web.Test.Common;

using Xunit;

namespace Web.Test;

/// <summary>
/// Integration tests. I am having some trouble finding a suitable naming system for these.
/// </summary>
[Collection(WebAppFactoryCollection.COLLECTION_NAME)]
public class WebTest {
    readonly CustomWebAppFactory _webapp;

    public WebTest(CustomWebAppFactory webapp) {
        _webapp = webapp;
    }

    [Fact]
    public async Task UnauthorizedUserStory_ReturnsUnauthorized() {
        var client = _webapp.ConfiguredClient;

        var expected = HttpStatusCode.Unauthorized;

        var response = await client.PostAsync("users/1/profiles/1/transactions", null);
        var actual = response.StatusCode;

        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task AuthorizedUserWrongIdStory_ReturnsForbidden() {
        var client = _webapp.ConfiguredClient;
        using var scope = _webapp.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<IAppDbContext>();
        await context.Database.EnsureCreatedAsync();

        // create user account

        (int userId, string jwtToken) = await client.CreateUser("wrongIdUser");

        // access resource with invalid ID

        var expected = HttpStatusCode.Forbidden;

        string url = $"users/{userId + 1}/profiles/1/transactions?amount=1&type=ALWAYS&paymentStart=2020-01-01";
        var message = new HttpRequestMessage(HttpMethod.Get, url).WithJwtCookie(jwtToken);
        var response = await client.SendAsync(message);
        var actual = response.StatusCode;

        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task AuthorizedUserWrongProfileStory_ReturnsNotFound() {
        var client = _webapp.ConfiguredClient;
        using var scope = _webapp.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<IAppDbContext>();
        await context.Database.EnsureCreatedAsync();

        // create 2 user accounts and 2 profiles

        (int firstUserId, string firstJwtToken) = await client.CreateUser("firstUser");
        (int firstProfileId, firstJwtToken) = await client.CreateProfile(firstUserId, firstJwtToken);
        (int secondUserId, string secondJwtToken) = await client.CreateUser("secondUser");
        (int secondProfileId, secondJwtToken) = await client.CreateProfile(secondUserId, secondJwtToken);

        // attempt to get transaction of second profile with first user

        var expected = HttpStatusCode.Forbidden;

        string url = $"users/{firstUserId}/profiles/{secondProfileId}/transactions";
        var message = new HttpRequestMessage(HttpMethod.Get, url).WithJwtCookie(firstJwtToken);
        var response = await client.SendAsync(message);
        var actual = response.StatusCode;

        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task ProfileDataDeletionStory_AuthorizesCreatesSoftDeletes() {
        var client = _webapp.ConfiguredClient;
        using var scope = _webapp.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<IAppDbContext>();
        await context.Database.EnsureCreatedAsync();

        // create user account and 2 profiles

        const string username = "softDeleteTestUser";
        (int userId, string jwtToken) = await client.CreateUser(username);
        (int profileOneId, jwtToken) = await client.CreateProfile(userId, jwtToken);
        (int profileTwoId, jwtToken) = await client.CreateProfile(userId, jwtToken);
        jwtToken = await client.SignInUser(username);

        // create 3 transactions per profile

        const int transactionsPerProfile = 3;
        HttpRequestMessage message;
        HttpResponseMessage response;
        foreach (int profileId in new[] { profileOneId, profileTwoId }) {
            for (int i = 0; i < transactionsPerProfile; i++) {
                const string transactionQuery = "amount=5&type=RETIREMENTONLY&paymentStart=2030-06-06&taxScheme=1";
                string transactionUrl = $"users/{userId}/profiles/{profileId}/transactions?{transactionQuery}";

                message = new HttpRequestMessage(HttpMethod.Post, transactionUrl).WithJwtCookie(jwtToken);
                response = await client.SendAsync(message);
                Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            }
        }

        // soft delete one of the profiles

        message = new HttpRequestMessage(HttpMethod.Delete, $"users/{userId}/profiles/{profileTwoId}").WithJwtCookie(jwtToken);
        response = await client.SendAsync(message);
        jwtToken = response.GetJwtToken(); // update jwt token profile list
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

        // confirm that soft deletion was applied correctly in DB

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

        // confirm that API only displays transactions for first profile

        message = new HttpRequestMessage(HttpMethod.Get, $"users/{userId}/profiles/{profileOneId}/transactions").WithJwtCookie(jwtToken);
        response = await client.SendAsync(message);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        message = new HttpRequestMessage(HttpMethod.Get, $"users/{userId}/profiles/{profileTwoId}/transactions").WithJwtCookie(jwtToken);
        response = await client.SendAsync(message);
        Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode); // profile not found (since it is deleted)
    }
}
