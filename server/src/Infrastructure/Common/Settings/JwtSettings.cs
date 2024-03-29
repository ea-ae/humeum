﻿namespace Infrastructure.Common.Settings;

public class JwtSettings {
    public required string Key { get; set; }
    public required string Issuer { get; set; }
    public required string Cookie { get; set; }
    public required int CookieExpireMinutes { get; set; }
    public required string RefreshCookie { get; set; }
    public required int RefreshCookieExpireDays { get; set; }
}
