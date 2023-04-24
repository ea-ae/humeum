using System.ComponentModel.DataAnnotations;

using Application.Common.Extensions;
using Application.Common.Interfaces;

using AutoMapper;

using Microsoft.EntityFrameworkCore;

using Shared.Interfaces;

namespace Application.Profiles.Queries;

public record GetProfileQuery : IQuery<IResult<ProfileDto, IBaseException>> {
    [Required] public int Profile { get; init; }
}

public class GetProfileQueryHandler : IQueryHandler<GetProfileQuery, IResult<ProfileDto, IBaseException>> {
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public GetProfileQueryHandler(IAppDbContext context, IMapper mapper) {
        _context = context;
        _mapper = mapper;
    }

    public Task<IResult<ProfileDto, IBaseException>> Handle(GetProfileQuery request, CancellationToken _) {
        var profile = _context.Profiles.AsNoTracking()
                                       .FirstOrDefault(p => p.Id == request.Profile && p.DeletedAt == null);

        var result = _mapper.ToMappedResultOrNotFound<Domain.ProfileAggregate.Profile, ProfileDto>(profile);
        return Task.FromResult(result);
    }
}
