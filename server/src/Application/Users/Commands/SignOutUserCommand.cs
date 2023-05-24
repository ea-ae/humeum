using System.ComponentModel.DataAnnotations;

using Application.Common.Interfaces;

using Shared.Interfaces;
using Shared.Models;

namespace Application.Users.Commands;

public record SignOutUserCommand : ICommand<IResult<None, IBaseException>> { }

public class SignOutUserCommandHandler : ICommandHandler<SignOutUserCommand, IResult<None, IBaseException>> {
    private readonly IApplicationUserService _userService;

    public SignOutUserCommandHandler(IApplicationUserService userService) {
        _userService = userService;
    }

    public async Task<IResult<None, IBaseException>> Handle(SignOutUserCommand request, CancellationToken token) {
        return await _userService.SignOutUserAsync();
    }
}
