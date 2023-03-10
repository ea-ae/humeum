using System.ComponentModel.DataAnnotations;

using Application.Common.Exceptions;
using Application.Common.Interfaces;

using AutoMapper;

using Microsoft.EntityFrameworkCore;

namespace Application.Profiles.Queries;

public record GetProfileQuery : IQuery<ProfileDto> {
    [Required] public int User { get; init; }
    [Required] public int Profile { get; init; }
}

public class GetProfileQueryHandler : IQueryHandler<GetProfileQuery, ProfileDto> {
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public GetProfileQueryHandler(IAppDbContext context, IMapper mapper) {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ProfileDto> Handle(GetProfileQuery request, CancellationToken _) {
        var profile = _context.Profiles.AsNoTracking()
                                       .FirstOrDefault(p => p.Id == request.Profile && p.UserId == request.User && p.DeletedAt == null);

        if (profile is null) {
            throw new NotFoundValidationException(typeof(Profile));
        }

        return await Task.Run(() => _mapper.Map<ProfileDto>(profile));
    }
}
