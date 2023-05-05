using System.ComponentModel.DataAnnotations;

using Application.Common.Extensions;
using Application.Common.Interfaces;

using AutoMapper;

using Domain.V1.AssetAggregate;

using Microsoft.EntityFrameworkCore;

using Shared.Interfaces;

namespace Application.V1.Assets.Queries;

public record GetAssetQuery : IQuery<IResult<AssetDto, IBaseException>>
{
    [Required] public required int Profile { get; init; }
    [Required] public required int Asset { get; init; }
}

public class GetAssetQueryHandler : IQueryHandler<GetAssetQuery, IResult<AssetDto, IBaseException>>
{
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public GetAssetQueryHandler(IAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public Task<IResult<AssetDto, IBaseException>> Handle(GetAssetQuery request, CancellationToken token = default)
    {
        var assetResult = _context.Assets.AsNoTracking()
                                         .Include(a => a.Type)
                                         .ToFoundResult(a => a.Id == request.Asset
                                                             && (a.ProfileId == request.Profile || a.ProfileId == null)
                                                             && a.DeletedAt == null);

        var result = assetResult.Then(asset => _mapper.MapToResult<AssetDto>(asset));
        return Task.FromResult(result);
    }
}
