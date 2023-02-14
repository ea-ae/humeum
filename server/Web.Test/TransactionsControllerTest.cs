using System.Net;

using Web.Test.Common;

using Xunit;

namespace Web.Test;

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
    public async Task AddAction_SimpleTransaction_ReturnsCreated() {
        const string url = "users/1/profiles/1/transactions?amount=5&type=RETIREMENTONLY&paymentStart=2030-06-06";
        var client = _webapp.ConfiguredClient;

        var expected = HttpStatusCode.Created;

        var response = await client.PostAsync(url, null);
        var actual = response.StatusCode;

        Assert.Equal(expected, actual);
    }
}
