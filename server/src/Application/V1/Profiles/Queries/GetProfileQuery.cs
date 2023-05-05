using System.ComponentModel.DataAnnotations;

using Application.Common.Extensions;
using Application.Common.Interfaces;

using AutoMapper;

using Microsoft.EntityFrameworkCore;

using Shared.Interfaces;

namespace Application.V1.Profiles.Queries;

public record GetProfileQuery : IQuery<IResult<ProfileDto, IBaseException>>
{
    [Required] public int Profile { get; init; }
}

public class GetProfileQueryHandler : IQueryHandler<GetProfileQuery, IResult<ProfileDto, IBaseException>>
{
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public GetProfileQueryHandler(IAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public Task<IResult<ProfileDto, IBaseException>> Handle(GetProfileQuery request, CancellationToken _)
    {
        var profileResult = _context.Profiles.AsNoTracking()
                                             .ToFoundResult(p => p.Id == request.Profile && p.DeletedAt == null);

        var result = profileResult.Then(profile => _mapper.MapToResult<ProfileDto>(profile));
        return Task.FromResult(result);
    }
}
