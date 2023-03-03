using System.ComponentModel.DataAnnotations;

using Application.Common.Interfaces;

using Domain.ProfileAggregate;

namespace Application.Profiles.Commands;

public record AddProfileCommand : ICommand<int> {
    [Required] public required int User { get; init; }

    [Required] public required string Name { get; init; }
    public string? Description { get; init; }
    public decimal? WithdrawalRate { get; init; }
}

public class AddProfileCommandHandler : ICommandHandler<AddProfileCommand, int> {
    private readonly IAppDbContext _context;
    private readonly IApplicationUserService _userService;

    public AddProfileCommandHandler(IAppDbContext context, IApplicationUserService userService) {
        _context = context;
        _userService = userService;
    }

    public async Task<int> Handle(AddProfileCommand request, CancellationToken token = default) {
        Profile profile = new(request.User, request.Name, request.Description, request.WithdrawalRate);

        _context.Profiles.Add(profile);
        await _context.SaveChangesAsync(token);
        await _userService.UpdateClientToken(request.User); // token should reflect a valid list of profiles

        return profile.Id;
    }
}
