using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Models;

public class ApplicationUser : IdentityUser<int> {
    public required string DisplayName { get; set; }
}
