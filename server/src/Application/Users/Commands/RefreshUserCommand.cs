using System.ComponentModel.DataAnnotations;

using Application.Common.Interfaces;

namespace Application.Users.Commands;

public record RefreshUserCommand : ICommand<int> {
    [Required] public required int User { get; init; }
    [Required] public required string RefreshToken { get; init; }
}

public class RefreshUserCommandHandler : ICommandHandler<RefreshUserCommand, int> {
    private readonly IApplicationUserService _userService;

    public RefreshUserCommandHandler(IApplicationUserService userService) => _userService = userService;

    public async Task<int> Handle(RefreshUserCommand request, CancellationToken token) {
        return await _userService.RefreshUserAsync(request.User, request.RefreshToken);
    }
}
