﻿namespace Infrastructure.Common.Settings;

public class DatabaseSettings {
    public required string Database { get; init; }
    public required string Name { get; init; }
    public required string Host { get; init; }
    public required string Username { get; init; }
    public required string Password { get; init; }
}
