using System.ComponentModel.DataAnnotations;

using Application.Common.Extensions;
using Application.Common.Interfaces;

using AutoMapper;
using AutoMapper.QueryableExtensions;

using Microsoft.EntityFrameworkCore;

namespace Application.Assets.Queries;

public record GetAssetsQuery: IQuery<List<AssetDto>> {
    [Required] public required int User { get; init; }
    [Required] public required int Profile { get; init; }
}

public class GetAssetsQueryHandler : IQueryHandler<GetAssetsQuery, List<AssetDto>> {
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public GetAssetsQueryHandler(IAppDbContext context, IMapper mapper) {
        _context = context;
        _mapper = mapper;
    }

    public Task<List<AssetDto>> Handle(GetAssetsQuery request, CancellationToken token = default) {
        _context.AssertUserOwnsProfile(request.User, request.Profile);

        var assets = _context.Assets.AsNoTracking().Include(a => a.Type)
            .Where(a => (a.ProfileId == request.Profile || a.ProfileId == null) && a.DeletedAt == null)
            .OrderBy(a => a.Id);

        var assetDtos = assets.ProjectTo<AssetDto>(_mapper.ConfigurationProvider).ToList();

        return Task.Run(() => assetDtos);
    }
}
