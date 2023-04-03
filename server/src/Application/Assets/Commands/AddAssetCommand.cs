using System.ComponentModel.DataAnnotations;

using Application.Common.Extensions;
using Application.Common.Interfaces;

using Domain.AssetAggregate;
using Domain.Common.Interfaces;
using Domain.Common.Models;
using Domain.TransactionAggregate.ValueObjects;

namespace Application.Assets.Commands;

public record AddAssetCommand : ICommand<IResult<int>> {
    [Required] public required int Profile { get; init; }

    [Required] public required string Name { get; init; }
    public required string? Description { get; init; }
    [Required] public required decimal? ReturnRate { get; init; }
    [Required] public required decimal? StandardDeviation { get; init; }
    [Required] public required string AssetType { get; init; }
}

public class AddAssetCommandHandler : ICommandHandler<AddAssetCommand, IResult<int>> {
    readonly IAppDbContext _context;

    public AddAssetCommandHandler(IAppDbContext context) => _context = context;

    public async Task<IResult<int>> Handle(AddAssetCommand request, CancellationToken token = default) {
        var assetType = _context.GetEnumerationEntityByCode<AssetType>(request.AssetType);
        var asset = new Asset(request.Name,
                              request.Description,
                              (decimal)request.ReturnRate!,
                              (decimal)request.StandardDeviation!,
                              assetType.Id,
                              request.Profile);

        _context.Assets.Add(asset);
        await _context.SaveChangesAsync(token);

        return Result<int>.Ok(asset.Id);
    }
}
