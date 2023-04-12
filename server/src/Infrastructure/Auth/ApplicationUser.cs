using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Auth;

public class ApplicationUser : IdentityUser<int> {
    public required string DisplayName { get; set; }

    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiry { get; set; }
}
