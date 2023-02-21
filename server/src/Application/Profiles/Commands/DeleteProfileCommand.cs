using System.ComponentModel.DataAnnotations;

using Application.Common.Exceptions;
using Application.Common.Interfaces;

using Domain.ProfileAggregate;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Application.Profiles.Commands.DeleteProfile;

/// <summary>
/// Create a new profile for a user. Users can have multiple profiles with their own
/// configurations and transactions.
/// </summary>
public record DeleteProfileCommand : ICommand {
    [Required] public required int User { get; init; }
    [Required] public required int Profile { get; init; }
}

public class DeleteProfileCommandHandler : ICommandHandler<DeleteProfileCommand> {
    private readonly IAppDbContext _context;

    public DeleteProfileCommandHandler(IAppDbContext context) => _context = context;

    public async Task<Unit> Handle(DeleteProfileCommand request, CancellationToken token) {
        var profile = _context.Profiles.Include(p => p.Transactions)
                                       .Where(p => p.Id == request.Profile && p.UserId == request.User && p.DeletedAt == null)
                                       .FirstOrDefault();

        if (profile is null) {
            throw new NotFoundValidationException(typeof(Profile));
        }

        _context.Profiles.Remove(profile);
        await _context.SaveChangesAsync(token);

        return Unit.Value;
    }
}
