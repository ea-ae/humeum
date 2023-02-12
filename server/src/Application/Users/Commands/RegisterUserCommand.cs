using Application.Common.Interfaces;

using Domain.UserAggregate;
using Domain.UserAggregate.ValueObjects;

namespace Application.Users.Commands.RegisterUser;

public record RegisterUserCommand : ICommand<int> {
    public required string Username { get; init; }
    public required string Email { get; init; }
    public required string Password { get; init; }
    public required string ConfirmPassword { get; init; }
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
            throw new Common.Exceptions.ValidationException("Password is too long (over 200 characters).");
        }

        if (request.Password != request.ConfirmPassword) {
            throw new Common.Exceptions.ValidationException("Passwords do not match.");
        }

        // handling

        var user = new User(request.Username, request.Email);

        return await _userService.CreateUserAsync(request.Username, request.Email, request.Password, request.RememberMe);
    }
}
