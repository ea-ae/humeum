using Application.Common.Interfaces;

using Domain.ProfileAggregate;

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
        Profile profile = _context.Profiles.Where(p => p.Id == request.Profile).Include(p => p.Transactions).Single();
        _context.Profiles.Remove(profile);
        await _context.SaveChangesAsync(token);

        return Unit.Value;
    }
}
