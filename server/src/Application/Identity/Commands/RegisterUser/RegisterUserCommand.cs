using System.ComponentModel.DataAnnotations;
using System.Net;

using Application.Common.Interfaces;

namespace Application.Transactions.Commands.AddTransaction;

public record RegisterUserCommand : ICommand<int> {
    
}

public class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand, int> {
    private readonly IAppDbContext _context;
    private readonly IApplicationUserService _userService;

    public RegisterUserCommandHandler(IAppDbContext context, IApplicationUserService userService) {
        _context = context;
        _userService = userService;
    }

    public async Task<int> Handle(RegisterUserCommand request, CancellationToken token) {


        return -1;
    }
}
