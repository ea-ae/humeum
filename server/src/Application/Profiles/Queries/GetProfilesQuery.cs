using System.ComponentModel.DataAnnotations;

using Application.Common.Interfaces;

using AutoMapper;
using AutoMapper.QueryableExtensions;

using Microsoft.EntityFrameworkCore;

namespace Application.Profiles.Queries.GetProfilesQuery;

/// <summary>
/// Get list of profiles owned by user.
/// </summary>
public record GetProfilesQuery : IQuery<List<ProfileDto>> {
    [Required] public required int User { get; init; }
}

public class GetProfilesQueryHandler : IQueryHandler<GetProfilesQuery, List<ProfileDto>> {
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public GetProfilesQueryHandler(IAppDbContext context, IMapper mapper) {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<ProfileDto>> Handle(GetProfilesQuery request, CancellationToken token) {
        var profiles = _context.Profiles.AsNoTracking().Where(p => p.UserId == request.User && p.DeletedAt == null);

        return await profiles.ProjectTo<ProfileDto>(_mapper.ConfigurationProvider).ToListAsync(token);
    }
}
