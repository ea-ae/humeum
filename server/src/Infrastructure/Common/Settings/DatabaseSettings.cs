namespace Infrastructure.Common.Settings;

public class DatabaseSettings {
    public required string Name { get; set; }
    public required string Host { get; set; }
    public required string Username { get; set; }
    public required string Password { get; set; }
}
