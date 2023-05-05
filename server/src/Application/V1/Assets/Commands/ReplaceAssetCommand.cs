using System.ComponentModel.DataAnnotations;

using Application.Common.Exceptions;
using Application.Common.Extensions;
using Application.Common.Interfaces;
using Domain.V1.AssetAggregate;
using Domain.V1.AssetAggregate.ValueObjects;
using Shared.Interfaces;
using Shared.Models;

namespace Application.V1.Assets.Commands;

public record ReplaceAssetCommand : ICommand<IResult<None, IBaseException>>
{
    [Required] public required int Profile { get; init; }
    [Required] public required int Asset { get; init; }

    [Required] public required string Name { get; init; }
    public required string? Description { get; init; }
    [Required] public required decimal? ReturnRate { get; init; }
    [Required] public required decimal? StandardDeviation { get; init; }
    [Required] public required string AssetType { get; init; }
}

public class ReplaceAssetCommandHandler : ICommandHandler<ReplaceAssetCommand, IResult<None, IBaseException>>
{
    readonly IAppDbContext _context;

    public ReplaceAssetCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<IResult<None, IBaseException>> Handle(ReplaceAssetCommand request, CancellationToken token = default)
    {
        // validate that the specified asset exists

        var assetType = _context.GetEnumerationEntityByCode<AssetType>(request.AssetType);

        if (assetType.Failure)
        {
            return Result<None, NotFoundValidationException>.Fail(new NotFoundValidationException(nameof(assetType)));
        }

        var asset = _context.Assets.FirstOrDefault(t => t.Id == request.Asset
                                                        && t.ProfileId == request.Profile
                                                        && t.DeletedAt == null);
        if (asset is null)
        {
            return Result<None, IBaseException>.Fail(new NotFoundValidationException(typeof(Asset)));
        }

        // update the asset

        var result = asset.Replace(request.Name, request.Description, (decimal)request.ReturnRate!, (decimal)request.StandardDeviation!, assetType.Unwrap().Id);

        return await result.ThenAsync<None, IBaseException>(async _ =>
        {
            await _context.SaveChangesWithHardDeletionAsync(token);
            return Result<None, IBaseException>.Ok(None.Value);
        });
    }
}
