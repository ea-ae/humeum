using Application.Common.Exceptions;
using Application.Common.Interfaces;

using Infrastructure.Identity;

using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Services;

public class ApplicationUserService : IApplicationUserService {
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public ApplicationUserService(UserManager<ApplicationUser> userManager, 
                                  SignInManager<ApplicationUser> signInManager) {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<int> CreateUserAsync(string username, string email, string password, bool rememberMe) {
        // todo automapper
        var appUser = new ApplicationUser {
            DisplayName = username,
            UserName = username,
            Email = email,
        };

        var result = await _userManager.CreateAsync(appUser, password);
        if (result.Succeeded) {
            await _signInManager.SignInAsync(appUser, isPersistent: rememberMe);
            var user = await _userManager.FindByEmailAsync(email);
            return user!.Id;
        }

        var error = result.Errors.First();
        throw new AuthenticationException($"{error.Code}\n{error.Description}");
    }

    public async Task<int> SignInUserAsync(string username, string password, bool rememberMe) {
        var result = await _signInManager.PasswordSignInAsync(username, password, isPersistent: rememberMe,
                                                              lockoutOnFailure: true);

        if (result.Succeeded) {
            var user = await _userManager.FindByNameAsync(username);
            return user!.Id;
        }

        if (result.IsLockedOut) {
            throw new AuthenticationException("Too many attempts, try again later.");
        }

        throw new AuthenticationException("Sign-in attempt failed.");
    }
}
