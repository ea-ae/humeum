using System.ComponentModel.DataAnnotations;

using Application.Common.Extensions;
using Application.Common.Interfaces;

using MediatR;

using Microsoft.EntityFrameworkCore;

using Shared.Interfaces;
using Shared.Models;

namespace Application.Profiles.Commands;

public record DeleteProfileCommand : ICommand<IResult<None, IBaseException>> {
    [Required] public required int User { get; init; }
    [Required] public required int Profile { get; init; }
}

public class DeleteProfileCommandHandler : ICommandHandler<DeleteProfileCommand, IResult<None, IBaseException>> {
    private readonly IAppDbContext _context;
    private readonly IApplicationUserService _userService;

    public DeleteProfileCommandHandler(IAppDbContext context, IApplicationUserService userService) {
        _context = context;
        _userService = userService;
    }

    public async Task<IResult<None, IBaseException>> Handle(DeleteProfileCommand request, CancellationToken token = default) {
        var profile = _context.Profiles.Include(p => p.Transactions)
                                       .Include(p => p.TransactionCategories)
                                       .Include(p => p.Assets)
                                       .ToFoundResult(p => p.Id == request.Profile && p.UserId == request.User && p.DeletedAt == null);

        return await profile.ThenAsync<None, IBaseException>(async p => {
            _context.Profiles.Remove(p);
            await _context.SaveChangesAsync(token);
            await _userService.UpdateClientToken(request.User); // token should reflect a valid list of profiles

            return Result<None, IBaseException>.Ok(None.Value);
        });
    }
}
