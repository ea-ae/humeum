using System.Net;

using Microsoft.AspNetCore.Mvc.Testing;

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
        var expected = HttpStatusCode.BadRequest;

        var client = _webapp.ConfiguredClient;
        var response = await client.PostAsync("users/1/profiles/1/transactions?amount=5", null);
        var actual = response.StatusCode;

        Assert.Equal(expected, actual);
    }
}
