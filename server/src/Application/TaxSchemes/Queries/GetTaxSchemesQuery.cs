using Application.Common.Interfaces;

using AutoMapper;
using AutoMapper.QueryableExtensions;

using Microsoft.EntityFrameworkCore;

namespace Application.TaxSchemes.Queries;

public record GetTaxSchemesQuery : IQuery<List<TaxSchemeDto>> { }

public class GetTaxSchemesQueryHandler : IQueryHandler<GetTaxSchemesQuery, List<TaxSchemeDto>> {
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public GetTaxSchemesQueryHandler(IAppDbContext context, IMapper mapper) {
        _context = context;
        _mapper = mapper;
    }

    public Task<List<TaxSchemeDto>> Handle(GetTaxSchemesQuery request, CancellationToken token = default) {
        var taxSchemes = _context.TaxSchemes.AsNoTracking();

        return Task.Run(() => taxSchemes.ProjectTo<TaxSchemeDto>(_mapper.ConfigurationProvider).ToList());
    }
}
