using System.ComponentModel.DataAnnotations;

using Application.Common.Exceptions;
using Application.Common.Extensions;
using Application.Common.Interfaces;
using Application.TransactionCategories.Queries;

using AutoMapper;
using AutoMapper.QueryableExtensions;

using Domain.TransactionAggregate;

using Microsoft.EntityFrameworkCore;

namespace Application.Transactions.Queries.GetCategories;

public record GetCategoriesQuery : IQuery<List<CategoryDto>> {
    [Required] public required int User { get; init; }
    [Required] public required int Profile { get; init; }
}

public class GetCategoriesQueryHandler : IQueryHandler<GetCategoriesQuery, List<CategoryDto>> {
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public GetCategoriesQueryHandler(IAppDbContext context, IMapper mapper) {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<CategoryDto>> Handle(GetCategoriesQuery request, CancellationToken token = default) {
        var categories = _context.TransactionCategories.AsNoTracking().Include(t => t.Profile)
            .Where(tc => ((tc.ProfileId == request.Profile && tc.Profile!.UserId == request.User) || tc.ProfileId == null)
                         && tc.DeletedAt == null)
            .OrderBy(tc => tc.Id);

        if (categories is null) {
            _context.AssertUserOwnsProfile(request.User, request.Profile);
        }

        return await categories.ProjectTo<CategoryDto>(_mapper.ConfigurationProvider).ToListAsync(token);
    }
}

