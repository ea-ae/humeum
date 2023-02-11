using System.ComponentModel.DataAnnotations;
using System.Net;

using Application.Common.Interfaces;

namespace Application.Transactions.Commands.AddTransaction;

public record RegisterUserCommand : ICommand<int> {
    public required string Username { get; init; }
    public required string Email { get; init; }
    public required string Password { get; init; }
    public required string ConfirmPassword { get; init; }
    public bool RememberMe { get; init; } = false;
}

public class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand, int> {
    private readonly IAppDbContext _context;
    private readonly IApplicationUserService _userService;

    public RegisterUserCommandHandler(IAppDbContext context, IApplicationUserService userService) {
        _context = context;
        _userService = userService;
    }

    public async Task<int> Handle(RegisterUserCommand request, CancellationToken token) {
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

        var userId = await _userService.CreateUserAsync(request.Username,
                                                        request.Email,
                                                        request.Password,
                                                        request.RememberMe);

        return userId;
    }
}
