namespace Web.Test.Common;

public static class AuthenticationExtensions {
    /// <summary>
    /// Gets a JWT authentication token from a register/login endpoint response.
    /// </summary>
    /// <returns>Whether a JWT token was found in the response.</returns>
    public static bool TryGetJwtToken(this HttpResponseMessage? message, out string? jwtToken) {
        jwtToken = null;

        if (message is null) {
            return false;
        }

        var setCookieHeaderFound = message.Headers.TryGetValues("Set-Cookie", out var cookies);
        
        if (!setCookieHeaderFound) {
            return false;
        }

        foreach (var cookie in cookies ?? Array.Empty<string>()) {
            if (cookie.StartsWith("Jwt")) {
                jwtToken = cookie.Split("=")[1].Split(";")[0];
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// Adds a JWT authentication cookie to a request.
    /// </summary>
    public static HttpRequestMessage WithJwtCookie(this HttpRequestMessage message, string jwtToken) {
        message.Headers.Add("Cookie", $"Jwt={jwtToken}");
        return message;
    }
}
