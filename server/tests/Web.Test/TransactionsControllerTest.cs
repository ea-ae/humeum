using System.Net;

using Microsoft.Extensions.DependencyInjection;

using Web.Test.Common;

using Xunit;
using Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Xunit.Abstractions;
using System.Security.Policy;

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
    public async Task AddAction_UnauthorizedUser_ReturnsUnauthorized() {
        string query = "users/1/profiles/1/transactions";
        var client = _webapp.ConfiguredClient;

        var expected = HttpStatusCode.Unauthorized;

        var response = await client.PostAsync(query, null);
        var actual = response.StatusCode;

        Assert.Equal(expected, actual);
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
    public async Task AddAction_CreateProfileAndTransactionsAndDeleteProfile_AuthorizesCreatesSoftDeletes() {
        var client = _webapp.ConfiguredClient;
        using var scope = _webapp.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<IAppDbContext>();
        //await context.Database.EnsureDeletedAsync();
        await context.Database.EnsureCreatedAsync();

        // create user account

        const string userQuery = "username=test&password=testingtesting&confirmPassword=testingtesting&email=test@test.com";
        var response = await client.PostAsync($"users/register?{userQuery}", null);
        Assert.NotNull(response.Headers.Location);
        int userId = int.Parse(response.Headers.Location.ToString().Split("/").Last());

        string jwtToken;
        Assert.True(response.TryGetJwtToken(out jwtToken!));

        // create 2 profiles

        var message = new HttpRequestMessage(HttpMethod.Post, $"users/{userId}/profiles?name=Default").WithJwtCookie(jwtToken);
        response = await client.SendAsync(message);
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.NotNull(response.Headers.Location);
        int profileOneId = int.Parse(response.Headers.Location.ToString().Split("/").Last());

        message = new HttpRequestMessage(HttpMethod.Post,
                                         $"users/{userId}/profiles?name=Conservative&withdrawalRate=3.15").WithJwtCookie(jwtToken);
        response = await client.SendAsync(message);
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.NotNull(response.Headers.Location);
        int profileTwoId = int.Parse(response.Headers.Location.ToString().Split("/").Last());

        // create 3 transactions per profile

        const int transactionsPerProfile = 3;
        foreach (int profileId in new[] { profileOneId, profileTwoId }) {
            for (int i = 0; i < transactionsPerProfile; i++) {
                const string transactionQuery = "amount=5&type=RETIREMENTONLY&paymentStart=2030-06-06";
                message = new HttpRequestMessage(HttpMethod.Post,
                                                 $"users/{userId}/profiles/{profileId}/transactions?{transactionQuery}").WithJwtCookie(jwtToken);
                response = await client.SendAsync(message);
                Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            }
        }

        // soft delete one of the profiles

        message = new HttpRequestMessage(HttpMethod.Delete, $"users/1/profiles/{profileTwoId}").WithJwtCookie(jwtToken);
        response = await client.SendAsync(message);
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
