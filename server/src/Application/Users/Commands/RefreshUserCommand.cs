using System.ComponentModel.DataAnnotations;

using Application.Common.Interfaces;

using Shared.Interfaces;

namespace Application.Users.Commands;

public record RefreshUserCommand : ICommand<IResult<int, IBaseException>> { }

public class RefreshUserCommandHandler : ICommandHandler<RefreshUserCommand, IResult<int, IBaseException>> {
    private readonly IApplicationUserService _userService;

    public RefreshUserCommandHandler(IApplicationUserService userService) => _userService = userService;

    public async Task<IResult<int, IBaseException>> Handle(RefreshUserCommand request, CancellationToken token) {
        return await _userService.RefreshUserAsync();
    }
}
