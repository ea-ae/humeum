using System.ComponentModel.DataAnnotations;

using Application.Common.Exceptions;
using Application.Common.Interfaces;

using Domain.AssetAggregate;

using MediatR;

namespace Application.Assets.Commands;

public record DeleteAssetCommand : ICommand {
    [Required] public required int Profile { get; init; }
    [Required] public required int Asset { get; init; }
}

public class DeleteAssetCommandHandler : ICommandHandler<DeleteAssetCommand> {
    readonly IAppDbContext _context;

    public DeleteAssetCommandHandler(IAppDbContext context) => _context = context;

    public async Task<Unit> Handle(DeleteAssetCommand request, CancellationToken token = default) {
        var asset = _context.Assets.Where(a => a.Id == request.Asset && a.ProfileId == request.Profile && a.DeletedAt == null)
                                   .FirstOrDefault();

        if (asset is null) {
            throw new NotFoundValidationException(typeof(Asset));
        }

        _context.Assets.Remove(asset);
        await _context.SaveChangesAsync(token);

        return Unit.Value;
    }
}
