﻿using System.ComponentModel.DataAnnotations;

using Application.Common.Extensions;
using Application.Common.Interfaces;

using AutoMapper;

using Microsoft.EntityFrameworkCore;

using Shared.Interfaces;

namespace Application.V1.Assets.Queries;

public record GetAssetsQuery : IQuery<IResult<List<AssetDto>, IBaseException>>
{
    [Required] public required int Profile { get; init; }
}

public class GetAssetsQueryHandler : IQueryHandler<GetAssetsQuery, IResult<List<AssetDto>, IBaseException>>
{
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public GetAssetsQueryHandler(IAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public Task<IResult<List<AssetDto>, IBaseException>> Handle(GetAssetsQuery request, CancellationToken token = default)
    {
        var assets = _context.Assets.AsNoTracking().Include(a => a.Type)
            .Where(a => (a.ProfileId == request.Profile || a.ProfileId == null) && a.DeletedAt == null)
            .OrderBy(a => a.Id);

        var result = assets.ProjectToResult<AssetDto>(_mapper);
        return Task.FromResult(result);
    }
}
