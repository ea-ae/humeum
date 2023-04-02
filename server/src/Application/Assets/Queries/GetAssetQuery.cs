using System.ComponentModel.DataAnnotations;

using Application.Common.Exceptions;
using Application.Common.Interfaces;

using AutoMapper;

using Domain.AssetAggregate;
using Domain.Common.Interfaces;
using Domain.Common.Models;

using Microsoft.EntityFrameworkCore;

namespace Application.Assets.Queries;

public record GetAssetQuery : IQuery<IResult<AssetDto>> {
    [Required] public required int Profile { get; init; }
    [Required] public required int Asset { get; init; }
}

public class GetAssetQueryHandler : IQueryHandler<GetAssetQuery, IResult<AssetDto>> {
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public GetAssetQueryHandler(IAppDbContext context, IMapper mapper) {
        _context = context;
        _mapper = mapper;
    }

    public Task<IResult<AssetDto>> Handle(GetAssetQuery request, CancellationToken token = default) {
        var asset = _context.Assets.AsNoTracking()
                                   .Include(a => a.Type)
                                   .FirstOrDefault(a => a.Id == request.Asset
                                                        && (a.ProfileId == request.Profile || a.ProfileId == null)
                                                        && a.DeletedAt == null);

        if (asset is null) {
            //throw new NotFoundValidationException(typeof(Asset));
            IResult<AssetDto> failResult = Result<AssetDto>.Fail(new NotFoundValidationException(typeof(Asset)));
            return Task.FromResult(failResult);
        }

        AssetDto dto = _mapper.Map<AssetDto>(asset);
        IResult<AssetDto> result = Result<AssetDto>.Ok(dto);
        return Task.FromResult(result);
    }
}
