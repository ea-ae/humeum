using System.ComponentModel.DataAnnotations;

using Application.Common.Interfaces;

using Domain.ProfileAggregate;

using Shared.Interfaces;

namespace Application.Profiles.Commands;

public record AddProfileCommand : ICommand<IResult<int, IBaseException>> {
    public required int User { get; init; }
    [Required] public required string Name { get; init; }
    public string? Description { get; init; }
    public decimal? WithdrawalRate { get; init; }
}

public class AddProfileCommandHandler : ICommandHandler<AddProfileCommand, IResult<int, IBaseException>> {
    private readonly IAppDbContext _context;
    private readonly IApplicationUserService _userService;

    public AddProfileCommandHandler(IAppDbContext context, IApplicationUserService userService) {
        _context = context;
        _userService = userService;
    }

    public async Task<IResult<int, IBaseException>> Handle(AddProfileCommand request, CancellationToken token = default) {
        var profileResult = Profile.Create(request.User, request.Name, request.Description, request.WithdrawalRate);

        return await profileResult.ThenAsync(async profile => {
            _context.Profiles.Add(profile);
            await _context.SaveChangesAsync(token);

            var result = await _userService.UpdateClientToken(request.User); // token should reflect a valid list of profiles
            return result.Then(profile.Id);
        });
    }
}
