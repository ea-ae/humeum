using System.ComponentModel.DataAnnotations;

using Application.Common.Exceptions;
using Application.Common.Extensions;
using Application.Common.Interfaces;
using Application.TransactionCategories.Queries;

using AutoMapper;

using Domain.TransactionCategoryAggregate;

using Microsoft.EntityFrameworkCore;

namespace Application.Transactions.Queries;

public record GetCategoryQuery : IQuery<CategoryDto> {
    [Required] public required int Profile { get; init; }
    [Required] public required int Category { get; init; }
}

public class GetCategoryQueryHandler : IQueryHandler<GetCategoryQuery, CategoryDto> {
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public GetCategoryQueryHandler(IAppDbContext context, IMapper mapper) {
        _context = context;
        _mapper = mapper;
    }

    public Task<CategoryDto> Handle(GetCategoryQuery request, CancellationToken token = default) {
        var category = _context.TransactionCategories.AsNoTracking()
            .FirstOrDefault(tc => tc.Id == request.Category
                                  && (tc.ProfileId == request.Profile || tc.ProfileId == null)
                                  && tc.DeletedAt == null);

        if (category is null) {
            throw new NotFoundValidationException(typeof(TransactionCategory));
        }

        return Task.Run(() => _mapper.Map<CategoryDto>(category));
    }
}

