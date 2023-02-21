using Application.Common.Exceptions;
using Application.Common.Interfaces;

using AutoMapper;

using Microsoft.EntityFrameworkCore;

namespace Application.Profiles.Queries.GetUserProfileDetails;

public record GetUserProfileQuery : IQuery<ProfileDto> {
    public int User { get; init; }
    public int Profile { get; init; }
}

public class GetUserProfileQueryHandler : IQueryHandler<GetUserProfileQuery, ProfileDto> {
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public GetUserProfileQueryHandler(IAppDbContext context, IMapper mapper) {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ProfileDto> Handle(GetUserProfileQuery request, CancellationToken _) {
        var profile = _context.Profiles.AsNoTracking()
                                       .FirstOrDefault(p => p.Id == request.Profile && p.UserId == request.User && p.DeletedAt == null);

        if (profile is null) {
            throw new NotFoundValidationException("User profile does not exist.");
        }

        return await Task.Run(() => _mapper.Map<ProfileDto>(profile));
    }
}
