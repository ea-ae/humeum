using System.ComponentModel.DataAnnotations;

using Application.Common.Exceptions;
using Application.Common.Extensions;
using Application.Common.Interfaces;
using Application.TransactionCategories.Queries;

using AutoMapper;
using AutoMapper.QueryableExtensions;

using Domain.AssetAggregate;
using Domain.TransactionCategoryAggregate;

using Microsoft.EntityFrameworkCore;

namespace Application.Assets.Queries;

public record GetAssetQuery : IQuery<AssetDto> {
    [Required] public required int User { get; init; }
    [Required] public required int Profile { get; init; }
    [Required] public required int Asset { get; init; }
}

public class GetAssetQueryHandler : IQueryHandler<GetAssetQuery, AssetDto> {
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public GetAssetQueryHandler(IAppDbContext context, IMapper mapper) {
        _context = context;
        _mapper = mapper;
    }

    public Task<AssetDto> Handle(GetAssetQuery request, CancellationToken token = default) {
        var asset = _context.Assets.AsNoTracking().Include(a => a.Profile).Include(a => a.Type)
            .FirstOrDefault(a => a.Id == request.Asset
                                 && ((a.ProfileId == request.Profile && a.Profile!.UserId == request.User) || a.ProfileId == null)
                                 && a.DeletedAt == null);

        if (asset is null) {
            _context.AssertUserOwnsProfile(request.User, request.Profile);
            throw new NotFoundValidationException(typeof(Asset));
        }

        return Task.Run(() => _mapper.Map<AssetDto>(asset));
    }
}
