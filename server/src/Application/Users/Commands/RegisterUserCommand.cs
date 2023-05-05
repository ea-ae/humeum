using System.ComponentModel.DataAnnotations;

using Application.Common.Exceptions;
using Application.Common.Interfaces;

using Domain.V1.UserAggregate;

using Shared.Interfaces;

namespace Application.Users.Commands;

public record RegisterUserCommand : ICommand<IResult<int, IAuthenticationException>> {
    [Required] public required string Username { get; init; }
    [Required] public required string Email { get; init; }
    [Required] public required string Password { get; init; }
    [Required] public required string ConfirmPassword { get; init; }
    public bool RememberMe { get; init; } = false;
}

public class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand, IResult<int, IAuthenticationException>> {
    private readonly IApplicationUserService _userService;

    public RegisterUserCommandHandler(IApplicationUserService userService) => _userService = userService;

    public async Task<IResult<int, IAuthenticationException>> Handle(RegisterUserCommand request, CancellationToken token) {
        // validation

        if (request.Password.Length > 200) {
            throw new ApplicationValidationException("Password is too long (over 200 characters).");
        }

        if (request.Password != request.ConfirmPassword) {
            throw new ApplicationValidationException("Passwords do not match.");
        }

        // handling

        var user = new User(request.Username, request.Email);
        return await _userService.CreateUserAsync(user, request.Password, request.RememberMe);
    }
}
