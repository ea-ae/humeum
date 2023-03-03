using System.ComponentModel.DataAnnotations;

using Application.Common.Exceptions;
using Application.Common.Interfaces;

using Domain.ProfileAggregate;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Application.Profiles.Commands;

public record DeleteProfileCommand : ICommand {
    [Required] public required int User { get; init; }
    [Required] public required int Profile { get; init; }
}

public class DeleteProfileCommandHandler : ICommandHandler<DeleteProfileCommand> {
    private readonly IAppDbContext _context;
    private readonly IApplicationUserService _userService;

    public DeleteProfileCommandHandler(IAppDbContext context, IApplicationUserService userService) {
        _context = context;
        _userService = userService;
    }

    public async Task<Unit> Handle(DeleteProfileCommand request, CancellationToken token = default) {
        var profile = _context.Profiles.Include(p => p.Transactions)
                                       .Include(p => p.TransactionCategories)
                                       .Include(p => p.Assets)
                                       .Where(p => p.Id == request.Profile && p.UserId == request.User && p.DeletedAt == null)
                                       .FirstOrDefault();

        if (profile is null) {
            throw new NotFoundValidationException(typeof(Profile));
        }

        _context.Profiles.Remove(profile);
        await _context.SaveChangesAsync(token);
        await _userService.UpdateClientToken(request.User); // token should reflect a valid list of profiles

        return Unit.Value;
    }
}
