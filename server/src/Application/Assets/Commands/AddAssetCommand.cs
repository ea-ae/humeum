using System.ComponentModel.DataAnnotations;

using Application.Common.Extensions;
using Application.Common.Interfaces;

using Domain.AssetAggregate;
using Domain.TransactionAggregate.ValueObjects;

using Shared.Interfaces;
using Shared.Models;

namespace Application.Assets.Commands;

public record AddAssetCommand : ICommand<IResult<int, IBaseException>> {
    [Required] public required int Profile { get; init; }

    [Required] public required string Name { get; init; }
    public required string? Description { get; init; }
    [Required] public required decimal? ReturnRate { get; init; }
    [Required] public required decimal? StandardDeviation { get; init; }
    [Required] public required string AssetType { get; init; }
}

public class AddAssetCommandHandler : ICommandHandler<AddAssetCommand, IResult<int, IBaseException>> {
    readonly IAppDbContext _context;

    public AddAssetCommandHandler(IAppDbContext context) => _context = context;

    public async Task<IResult<int, IBaseException>> Handle(AddAssetCommand request, CancellationToken token = default) {
        var assetType = _context.GetEnumerationEntityByCode<AssetType>(request.AssetType);
        var asset = Asset.Create(request.Name,
                                 request.Description,
                                 (decimal)request.ReturnRate!,
                                 (decimal)request.StandardDeviation!,
                                 assetType.Unwrap().Id,
                                 request.Profile);

        return await asset.ThenAsync<int, IBaseException>(async value => {
            _context.Assets.Add(asset.Unwrap());
            await _context.SaveChangesAsync(token);
            return Result<int, IBaseException>.Ok(value.Id);
        });
    }
}
