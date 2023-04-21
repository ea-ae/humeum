using System.ComponentModel.DataAnnotations;

using Application.Common.Interfaces;

using Shared.Interfaces;

namespace Application.Users.Commands;

public record SignInUserCommand : ICommand<IResult<int, IAuthenticationException>> {
    [Required] public required string Username { get; init; }
    [Required] public required string Password { get; init; }
    public bool RememberMe { get; init; } = false;
}

public class SignInUserCommandHandler : ICommandHandler<SignInUserCommand, IResult<int, IAuthenticationException>> {
    private readonly IApplicationUserService _userService;

    public SignInUserCommandHandler(IApplicationUserService userService) {
        _userService = userService;
    }

    public async Task<IResult<int, IAuthenticationException>> Handle(SignInUserCommand request, CancellationToken token) {
        return await _userService.SignInUserAsync(request.Username, request.Password, request.RememberMe);
    }
}
