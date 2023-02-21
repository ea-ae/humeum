using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity;

public class ApplicationUser : IdentityUser<int>
{
    public required string DisplayName { get; set; }
}
