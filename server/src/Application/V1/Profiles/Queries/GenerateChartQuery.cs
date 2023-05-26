using System.ComponentModel.DataAnnotations;

using Application.Common.Extensions;
using Application.Common.Interfaces;

using AutoMapper;

using Domain.V1.ProfileAggregate.ValueObjects;

using Microsoft.EntityFrameworkCore;

using Shared.Interfaces;

namespace Application.V1.Profiles.Queries;

public record GenerateChartQuery : IQuery<IResult<ProjectionDto, IBaseException>> {
    [Required] public int Profile { get; init; }
    public DateTime? Until { get; init; }
}

public class GenerateChartQueryHandler : IQueryHandler<GenerateChartQuery, IResult<ProjectionDto, IBaseException>> {
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public GenerateChartQueryHandler(IAppDbContext context, IMapper mapper) {
        _context = context;
        _mapper = mapper;
    }

    public Task<IResult<ProjectionDto, IBaseException>> Handle(GenerateChartQuery request, CancellationToken _) {
        var profileResult = _context.Profiles.AsNoTracking()
                                             .Include(p => p.Transactions)
                                                .ThenInclude(t => t.Type)
                                             .Include(p => p.Transactions)
                                                .ThenInclude(t => t.TaxScheme)
                                             .Include(p => p.Transactions)
                                                .ThenInclude(t => t.Asset)
                                             .Include(p => p.Transactions)
                                                .ThenInclude(t => t.PaymentTimeline.Frequency)
                                                    .ThenInclude(f => f!.TimeUnit)
                                             .ToFoundResult(p => p.Id == request.Profile && p.DeletedAt == null);

        var projectionResult = profileResult.Then<Projection, IBaseException>(profile => {
            var until = DateOnly.FromDateTime(request.Until ?? new DateTime(2100, 1, 1));
            return profile.GenerateProjection(until);
        }).Then<ProjectionDto, IBaseException>(projection =>  _mapper.MapToResult<ProjectionDto>(projection));

        return Task.FromResult(projectionResult);
    }
}
