using Application.Common.Interfaces;

using Infrastructure.Identity;

using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Services;

public class ApplicationUserService : IApplicationUserService {
    private readonly UserManager<ApplicationUser> _userManager;

    public ApplicationUserService(UserManager<ApplicationUser> userManager) {
        _userManager= userManager;
    }
}
