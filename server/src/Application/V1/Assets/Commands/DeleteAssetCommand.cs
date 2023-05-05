using System.ComponentModel.DataAnnotations;

using Application.Common.Exceptions;
using Application.Common.Extensions;
using Application.Common.Interfaces;

using Domain.V1.AssetAggregate;

using Shared.Interfaces;
using Shared.Models;

namespace Application.V1.Assets.Commands;

public record DeleteAssetCommand : ICommand<IResult<None, IBaseException>>
{
    [Required] public required int Profile { get; init; }
    [Required] public required int Asset { get; init; }
}

public class DeleteAssetCommandHandler : ICommandHandler<DeleteAssetCommand, IResult<None, IBaseException>>
{
    readonly IAppDbContext _context;

    public DeleteAssetCommandHandler(IAppDbContext context) => _context = context;

    public async Task<IResult<None, IBaseException>> Handle(DeleteAssetCommand request, CancellationToken token = default)
    {
        var assetResult = _context.Assets.Where(a => a.Id == request.Asset && a.ProfileId == request.Profile && a.DeletedAt == null)
                                         .ToFoundResult();

        return await assetResult.ThenAsync<None, IBaseException>(async asset =>
        {
            _context.Assets.Remove(asset);
            await _context.SaveChangesAsync(token);
            return Result<None, IBaseException>.Ok(None.Value);
        });
    }
}
