using Application.Common.Interfaces;

namespace Application.Users.Commands.RegisterUser;

public record RegisterUserCommand : ICommand<string> {
    public required string Username { get; init; }
    public required string Email { get; init; }
    public required string Password { get; init; }
    public required string ConfirmPassword { get; init; }
    public bool RememberMe { get; init; } = false;
}

public class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand, string> {
    private readonly IApplicationUserService _userService;

    public RegisterUserCommandHandler(IApplicationUserService userService) {
        _userService = userService;
    }

    public async Task<string> Handle(RegisterUserCommand request, CancellationToken token) {
        // validation

        if (request.Username.Length > 20) { // todo: these will go in the domain later
            throw new Common.Exceptions.ValidationException("Username is too long (over 20 characters).");
        }

        if (request.Password.Length > 200) {
            throw new Common.Exceptions.ValidationException("Password is too long (over 200 characters).");
        }

        if (request.Password != request.ConfirmPassword) {
            throw new Common.Exceptions.ValidationException("Passwords do not match.");
        }

        // handling

        return await _userService.CreateUserAsync(request.Username, request.Email, request.Password, request.RememberMe);
    }
}
