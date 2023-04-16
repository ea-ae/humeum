using System.ComponentModel.DataAnnotations;

using Application.Common.Interfaces;

using Shared.Interfaces;

namespace Application.Users.Commands;

public record RefreshUserCommand : ICommand<IResult<int, IAuthenticationException>> {
    [Required] public required int User { get; init; }
}

public class RefreshUserCommandHandler : ICommandHandler<RefreshUserCommand, IResult<int, IAuthenticationException>> {
    private readonly IApplicationUserService _userService;

    public RefreshUserCommandHandler(IApplicationUserService userService) => _userService = userService;

    public async Task<IResult<int, IAuthenticationException>> Handle(RefreshUserCommand request, CancellationToken token) {
        return await _userService.RefreshUserAsync(request.User);
    }
}
