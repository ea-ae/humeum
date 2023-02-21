using System.ComponentModel.DataAnnotations;

using Application.Common.Exceptions;
using Application.Common.Interfaces;

using Domain.UserAggregate;

namespace Application.Users.Commands.RegisterUser;

public record RegisterUserCommand : ICommand<int> {
    [Required] public required string Username { get; init; }
    [Required] public required string Email { get; init; }
    [Required] public required string Password { get; init; }
    [Required] public required string ConfirmPassword { get; init; }
    public bool RememberMe { get; init; } = false;
}

public class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand, int> {
    private readonly IApplicationUserService _userService;

    public RegisterUserCommandHandler(IApplicationUserService userService) {
        _userService = userService;
    }

    public async Task<int> Handle(RegisterUserCommand request, CancellationToken token) {
        // validation

        if (request.Password.Length > 200) {
            throw new ApplicationValidationException("Password is too long (over 200 characters).");
        }

        if (request.Password != request.ConfirmPassword) {
            throw new ApplicationValidationException("Passwords do not match.");
        }

        // handling

        var user = new User(request.Username, request.Email);
        int userId = await _userService.CreateUserAsync(user, request.Password, request.RememberMe);

        return userId;
    }
}
