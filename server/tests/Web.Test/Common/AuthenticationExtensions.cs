using System.Net;
using System.Xml.Linq;

namespace Web.Test.Common;

public static class AuthenticationExtensions {
    /// <summary>
    /// Gets a JWT authentication token from a register/login endpoint response.
    /// </summary>
    public static string GetJwtToken(this HttpResponseMessage? message) {
        if (message is null) {
            throw new ArgumentException("No response was found.");
        }

        var setCookieHeaderFound = message.Headers.TryGetValues("Set-Cookie", out var cookies);

        if (!setCookieHeaderFound) {
            throw new ArgumentException("No cookie header was found.");
        }

        foreach (var cookie in cookies ?? Array.Empty<string>()) {
            if (cookie.StartsWith("Jwt")) {
                return cookie.Split("=")[1].Split(";")[0];
            }
        }

        throw new ArgumentException("No cookie with name 'Jwt' was found.");
    }

    /// <summary>
    /// Adds a JWT authentication cookie to a request.
    /// </summary>
    public static HttpRequestMessage WithJwtCookie(this HttpRequestMessage message, string jwtToken) {
        message.Headers.Add("Cookie", $"Jwt={jwtToken}");
        return message;
    }

    /// <summary>
    /// Creates a user with an HttpClient.
    /// </summary>
    /// <param name="client">HttpClient instance to make the API calls with.</param>
    /// <param name="name">Identificator used for username and email address.</param>
    /// <returns>ID and token of newly created user.</returns>
    public static async Task<(int userId, string jwtToken)> CreateUser(this HttpClient client, string name) {
        string userQuery = $"username={name}&password=testingtesting&confirmPassword=testingtesting&email={name}@test.com";
        var response = await client.PostAsync($"users/register?{userQuery}", null);

        if (response is null) {
            throw new InvalidOperationException("No response returned.");
        } else if (response.StatusCode != HttpStatusCode.Created) {
            throw new InvalidOperationException("User could not be created.");
        } else if (response.Headers.Location is null) {
            throw new InvalidOperationException("Location header was not returned by API endpoint.");
        }

        int userId = int.Parse(response.Headers.Location.ToString().Split("/").Last());
        string jwtToken = response.GetJwtToken();

        return (userId, jwtToken);
    }

    public static async Task<string> SignInUser(this HttpClient client, string name) {
        string userQuery = $"username={name}&password=testingtesting";
        var response = await client.PostAsync($"users/sign-in?{userQuery}", null);

        if (response is null) {
            throw new InvalidOperationException("No response returned.");
        } else if (response.StatusCode != HttpStatusCode.OK) {
            throw new InvalidOperationException("User could not be signed in.");
        }
        
        return response.GetJwtToken();
    }

    /// <summary>
    /// Creates a profile with an HttpClient.
    /// </summary>
    /// <param name="client">Httpclient instance to make the API calls with.</param>
    /// <param name="userId">ID of user to create the account for.</param>
    /// <param name="jwtToken">JWT token to use for authorization.</param>
    /// <returns>ID of newly created profile.</returns>
    public static async Task<int> CreateProfile(this HttpClient client, int userId, string jwtToken) {
        var message = new HttpRequestMessage(HttpMethod.Post, $"users/{userId}/profiles?name=Default").WithJwtCookie(jwtToken);
        var response = await client.SendAsync(message);

        if (response is null) {
            throw new InvalidOperationException("No response returned.");
        }
        if (response.StatusCode != HttpStatusCode.Created) {
            throw new InvalidOperationException("Profile could not be created.");
        }
        if (response.Headers.Location is null) {
            throw new InvalidOperationException("Location header was not returned by API endpoint.");
        }

        int profileId = int.Parse(response.Headers.Location.ToString().Split("/").Last());

        return profileId;
    }
}
