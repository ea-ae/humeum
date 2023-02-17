using System.Net.Http.Headers;

using Web.Test.Common;

using Xunit;

namespace Web.Test;

/// <summary>
/// End-to-end tests for admin actions that check authentication functionality.
/// </summary>
[Collection(WebAppFactoryCollection.COLLECTION_NAME)]
public class AdminControllerTest {
    readonly CustomWebAppFactory _webapp;

    // temp
    const string JWT = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy" +
                       "93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiZXhhbXBsZSIsImV4cCI6MTY3ODgxM" +
                       "jM4MCwiaXNzIjoiaHVtZXVtIiwiYXVkIjoiaHVtZXVtIn0.zynAH0JQVWXeDneWd2onJzdGV7aQzS17CmOkI2oA7JA";

    public AdminControllerTest(CustomWebAppFactory webapp) {
        _webapp = webapp;
    }

    [Fact]
    public async Task GetAuthorizationHeader_ValidHeader_ReturnsHeader() {
        const string expected = "Bearer " + JWT;
        var client = _webapp.ConfiguredClient;

        using var request = new HttpRequestMessage(HttpMethod.Get, "admin/authorization");
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", JWT);
        var response = await client.SendAsync(request);
        var actual = await response.Content.ReadAsStringAsync();

        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task GetAuthenticationStatus_InvalidJwtHeader_ReturnsFalse() {
        const string expected = "False";
        
        var client = _webapp.ConfiguredClient;

        using var request = new HttpRequestMessage(HttpMethod.Get, "admin/is-authenticated");
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", "123");
        var response = await client.SendAsync(request);
        var actual = await response.Content.ReadAsStringAsync();

        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task GetAuthenticationStatus_ValidJwtHeader_ReturnsTrue() {
        const string expected = "True";
        var client = _webapp.ConfiguredClient;

        using var request = new HttpRequestMessage(HttpMethod.Get, "admin/is-authenticated");
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", JWT);
        var response = await client.SendAsync(request);
        var actual = await response.Content.ReadAsStringAsync();

        Assert.Equal(expected, actual);
    }
}
