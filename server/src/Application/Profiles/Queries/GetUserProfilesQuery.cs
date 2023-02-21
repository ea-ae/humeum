﻿using Application.Common.Interfaces;

using AutoMapper;
using AutoMapper.QueryableExtensions;

using Microsoft.EntityFrameworkCore;

namespace Application.Profiles.Queries;

/// <summary>
/// Get list of profiles owned by user.
/// </summary>
public record GetUserProfilesQuery : IQuery<List<ProfileDto>> {
    public int User { get; init; }
}

public class GetUserProfilesQueryHandler : IQueryHandler<GetUserProfilesQuery, List<ProfileDto>> {
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public GetUserProfilesQueryHandler(IAppDbContext context, IMapper mapper) {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<ProfileDto>> Handle(GetUserProfilesQuery request, CancellationToken token) {
        var profiles = _context.Profiles.AsNoTracking().Where(p => p.UserId == request.User && p.DeletedAt == null);

        return await profiles.ProjectTo<ProfileDto>(_mapper.ConfigurationProvider).ToListAsync(token);
    }
}
