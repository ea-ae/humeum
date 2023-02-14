using System.Net;

using Microsoft.AspNetCore.Mvc.Testing;

using Web.Test.Common;

using Xunit;

namespace Web.Test;

[Collection(WebApplicationFactoryCollection.COLLECTION_NAME)]
public class TransactionsControllerTest {
    private readonly WebApplicationFactory<Program> _fixture;
    private readonly HttpClient _client;

    public TransactionsControllerTest(WebApplicationFactory<Program> fixture) {
        _fixture = fixture;

        fixture.ClientOptions.BaseAddress = new Uri("http://localhost/api/v1/");
        _client = _fixture.CreateClient(new WebApplicationFactoryClientOptions {
            BaseAddress = new Uri("http://localhost/api/v1/"),
            AllowAutoRedirect = false,
            HandleCookies = false
        });
        _client.DefaultRequestHeaders.Add("X-Requested-With", "HttpClient");
    }

    [Fact]
    public async Task AddAction_MissingQueryData_ReturnsBadRequest() {
        var expected = HttpStatusCode.BadRequest;

        var response = await _client.PostAsync("users/1/profiles/1/transactions?amount=5", null);
        var actual = response.StatusCode;

        

        Assert.Equal(expected, actual);
    }
}
