using System.ComponentModel.DataAnnotations;

using Application.Common.Extensions;
using Application.Common.Interfaces;

using AutoMapper;
using AutoMapper.QueryableExtensions;

using Microsoft.EntityFrameworkCore;

using Shared.Interfaces;

namespace Application.V1.Profiles.Queries;

/// <summary>
/// Get list of profiles owned by user.
/// </summary>
public record GetProfilesQuery : IQuery<IResult<List<ProfileDto>, IBaseException>>
{
    [Required] public required int User { get; init; }
}

public class GetProfilesQueryHandler : IQueryHandler<GetProfilesQuery, IResult<List<ProfileDto>, IBaseException>>
{
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public GetProfilesQueryHandler(IAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public Task<IResult<List<ProfileDto>, IBaseException>> Handle(GetProfilesQuery request, CancellationToken token)
    {
        var profiles = _context.Profiles.AsNoTracking().Where(p => p.UserId == request.User && p.DeletedAt == null);

        var result = profiles.ProjectToResult<ProfileDto>(_mapper);
        return Task.FromResult(result);
    }
}
