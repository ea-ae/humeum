using Application.Common.Interfaces;
using Shared.Interfaces;
using Shared.Models;

using AutoMapper;
using AutoMapper.QueryableExtensions;

using Microsoft.EntityFrameworkCore;

namespace Application.TaxSchemes.Queries;


public record GetTaxSchemesQuery : IQuery<IResult<List<TaxSchemeDto>, IBaseException>> { }

public class GetTaxSchemesQueryHandler : IQueryHandler<GetTaxSchemesQuery, IResult<List<TaxSchemeDto>, IBaseException>> {
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public GetTaxSchemesQueryHandler(IAppDbContext context, IMapper mapper) {
        _context = context;
        _mapper = mapper;
    }

    public Task<IResult<List<TaxSchemeDto>, IBaseException>> Handle(GetTaxSchemesQuery request, CancellationToken token = default) {
        var taxSchemes = _context.TaxSchemes.AsNoTracking();
        var taxSchemeDtos = taxSchemes.ProjectTo<TaxSchemeDto>(_mapper.ConfigurationProvider).ToList();

        IResult<List<TaxSchemeDto>, IBaseException> result = Result<List<TaxSchemeDto>, IBaseException>.Ok(taxSchemeDtos);
        return Task.FromResult(result);
    }
}
