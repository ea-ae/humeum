using System.ComponentModel.DataAnnotations;

using Application.Common.Exceptions;
using Application.Common.Interfaces;

using Domain.AssetAggregate;
using Domain.Common.Interfaces;
using Domain.Common.Models;

namespace Application.Assets.Commands;

public record DeleteAssetCommand : ICommand<IResult<None, IBaseException>> {
    [Required] public required int Profile { get; init; }
    [Required] public required int Asset { get; init; }
}

public class DeleteAssetCommandHandler : ICommandHandler<DeleteAssetCommand, IResult<None, IBaseException>> {
    readonly IAppDbContext _context;

    public DeleteAssetCommandHandler(IAppDbContext context) => _context = context;

    public async Task<IResult<None, IBaseException>> Handle(DeleteAssetCommand request, CancellationToken token = default) {
        var asset = _context.Assets.Where(a => a.Id == request.Asset && a.ProfileId == request.Profile && a.DeletedAt == null)
                                   .FirstOrDefault();

        if (asset is null) {
            return Result<None, IBaseException>.Fail(new NotFoundValidationException(typeof(Asset)));
        }

        _context.Assets.Remove(asset);
        await _context.SaveChangesAsync(token);

        return Result<None, IBaseException>.Ok(None.Value);
    }
}
