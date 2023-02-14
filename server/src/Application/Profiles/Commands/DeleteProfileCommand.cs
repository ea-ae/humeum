using Application.Common.Exceptions;
using Application.Common.Interfaces;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Application.Profiles.Commands.AddProfile;

/// <summary>
/// Create a new profile for a user. Users can have multiple profiles with their own
/// configurations and transactions.
/// </summary>
public record DeleteProfileCommand : ICommand {
    public required int Profile { get; init; }
}

public class DeleteProfileCommandHandler : ICommandHandler<DeleteProfileCommand> {
    private readonly IAppDbContext _context;

    public DeleteProfileCommandHandler(IAppDbContext context) => _context = context;

    public async Task<Unit> Handle(DeleteProfileCommand request, CancellationToken token) {
        var profile = _context.Profiles.Where(p => p.Id == request.Profile && p.DeletedAt == null)
                                       .Include(p => p.Transactions)
                                       .FirstOrDefault();

        if (profile == null) {
            throw new ValidationException("Profile with given ID does not exist.");
        }

        _context.Profiles.Remove(profile);
        await _context.SaveChangesAsync(token);

        return Unit.Value;
    }
}
