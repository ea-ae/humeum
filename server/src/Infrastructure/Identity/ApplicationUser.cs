using Microsoft.AspNetCore.Identity;

using Application.Common.Interfaces;
using Domain.UserAggregate;

namespace Infrastructure.Identity;

public class ApplicationUser : IdentityUser<int> {
    public required string DisplayName { get; set; }
}
