using System.ComponentModel.DataAnnotations;

using Application.Common.Interfaces;

using Domain.ProfileAggregate;

namespace Application.Profiles.Commands.AddProfile;

/// <summary>
/// Create a new profile for a user. Users can have multiple profiles with their own
/// configurations and transactions.
/// </summary>
public record AddProfileCommand : ICommand<int> {
    [Required] public required int User { get; init; }

    [Required] public required string Name { get; init; }
    public string? Description { get; init; }
    public decimal? WithdrawalRate { get; init; }
}

public class AddProfileCommandHandler : ICommandHandler<AddProfileCommand, int> {
    private readonly IAppDbContext _context;

    public AddProfileCommandHandler(IAppDbContext context) => _context = context;

    public async Task<int> Handle(AddProfileCommand request, CancellationToken token) {
        Profile profile = new(request.User!, request.Name, request.Description, request.WithdrawalRate);

        _context.Profiles.Add(profile);
        await _context.SaveChangesAsync(token);

        return profile.Id;
    }
}
