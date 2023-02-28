using System.ComponentModel.DataAnnotations;

using Application.Common.Exceptions;
using Application.Common.Extensions;
using Application.Common.Interfaces;

using Domain.AssetAggregate;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Application.Assets.Commands;

public record DeleteAssetCommand : ICommand {
    [Required] public required int User { get; init; }
    [Required] public required int Profile { get; init; }
    [Required] public required int Asset { get; init; }
}

public class DeleteAssetCommandHandler : ICommandHandler<DeleteAssetCommand> {
    readonly IAppDbContext _context;

    public DeleteAssetCommandHandler(IAppDbContext context) => _context = context;

    public async Task<Unit> Handle(DeleteAssetCommand request, CancellationToken token = default) {
        var asset = _context.Assets.Include(a => a.Profile)
            .Where(a => a.Id == request.Asset
                        && a.ProfileId == request.Profile 
                        && a.Profile!.UserId == request.User
                        && a.DeletedAt == null)
            .FirstOrDefault();

        if (asset is null) {
            _context.AssertUserOwnsProfile(request.User, request.Profile);
            throw new NotFoundValidationException(typeof(Asset));
        }

        _context.Assets.Remove(asset);
        await _context.SaveChangesAsync(token);

        return Unit.Value;
    }
}
