using System.ComponentModel.DataAnnotations;

using Application.Common.Interfaces;

namespace Application.Users.Commands;

public record SignInUserCommand : ICommand<int> {
    [Required] public required string Username { get; init; }
    [Required] public required string Password { get; init; }
    public bool RememberMe { get; init; } = false;
}

public class SignInUserCommandHandler : ICommandHandler<SignInUserCommand, int> {
    private readonly IApplicationUserService _userService;

    public SignInUserCommandHandler(IApplicationUserService userService) {
        _userService = userService;
    }

    public async Task<int> Handle(SignInUserCommand request, CancellationToken token) {
        return await _userService.SignInUserAsync(request.Username, request.Password, request.RememberMe);
    }
}
