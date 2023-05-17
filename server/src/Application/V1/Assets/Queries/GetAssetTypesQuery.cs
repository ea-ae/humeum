using Application.Common.Interfaces;

using AutoMapper;
using AutoMapper.QueryableExtensions;

using Domain.V1.AssetAggregate;

using Shared.Interfaces;
using Shared.Models;

namespace Application.V1.Assets.Queries;

public record GetAssetTypesQuery : IQuery<IResult<IEnumerable<AssetTypeDto>, IBaseException>> { }

public class GetAssetTypesQueryHandler : IQueryHandler<GetAssetTypesQuery, IResult<IEnumerable<AssetTypeDto>, IBaseException>> {
    IMapper _mapper;

    public GetAssetTypesQueryHandler(IMapper mapper) {
        _mapper = mapper;
    }

    public Task<IResult<IEnumerable<AssetTypeDto>, IBaseException>> Handle(GetAssetTypesQuery request, CancellationToken cancellationToken) {
        var types = Asset.Types.AsQueryable().ProjectTo<AssetTypeDto>(_mapper.ConfigurationProvider);
        IResult<IEnumerable<AssetTypeDto>, IBaseException> result = Result<IEnumerable<AssetTypeDto>, IBaseException>.Ok(types);
        return Task.FromResult(result);
    }
}
