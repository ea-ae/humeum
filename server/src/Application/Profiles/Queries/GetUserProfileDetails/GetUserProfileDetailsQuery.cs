using Application.Common.Exceptions;
using Application.Common.Interfaces;

using AutoMapper;

namespace Application.Profiles.Queries.GetUserProfileDetails;

public record GetUserProfileDetailsQuery : IQuery<ProfileDto> {
    public int User { get; init; }
    public int Profile { get; init; }
}

public class GetUserProfileDetailsQueryHandler : IQueryHandler<GetUserProfileDetailsQuery, ProfileDto> {
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public GetUserProfileDetailsQueryHandler(IAppDbContext context, IMapper mapper) {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ProfileDto> Handle(GetUserProfileDetailsQuery request, CancellationToken cancellationToken) {
        var profile = _context.Profiles.FirstOrDefault(p => p.Id == request.Profile && p.UserId == request.User);

        if (profile is null) {
            throw new NotFoundValidationException("User profile does not exist.");
        }

        return await Task.Run(() =>_mapper.Map<ProfileDto>(profile));
    }
}
