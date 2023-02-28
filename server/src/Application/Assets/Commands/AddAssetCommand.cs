using System.ComponentModel.DataAnnotations;

using Application.Common.Exceptions;
using Application.Common.Extensions;
using Application.Common.Interfaces;

using Domain.AssetAggregate;

namespace Application.Assets.Commands;

public record AddAssetCommand : ICommand<int> {
    [Required] public required int User { get; init; }
    [Required] public required int Profile { get; init; }
    
    [Required] public required string Name { get; init; }
    public required string? Description { get; init; }
    [Required] public required decimal? ReturnRate { get; init; }
    [Required] public required decimal? StandardDeviation { get; init; }
    [Required] public required int? AssetType { get; init; }
}

public class AddAssetCommandHandler : ICommandHandler<AddAssetCommand, int> {
    readonly IAppDbContext _context;

    public AddAssetCommandHandler(IAppDbContext context) => _context = context;

    public async Task<int> Handle(AddAssetCommand request, CancellationToken token = default) {
        // validation

        _context.AssertUserOwnsProfile(request.User, request.Profile);

        var assetTypeExists = _context.AssetTypes.Any(at => at.Id == request.AssetType);
        if (!assetTypeExists) {
            throw new NotFoundValidationException(typeof(Asset));
        }

        // handling

        var asset = new Asset(request.Name,
                              request.Description,
                              (decimal)request.ReturnRate!,
                              (decimal)request.StandardDeviation!,
                              (int)request.AssetType!,
                              request.Profile);
        _context.Assets.Add(asset);
        await _context.SaveChangesAsync(token);

        return asset.Id;
    }
}
