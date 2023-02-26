﻿using System.ComponentModel.DataAnnotations;

using Application.Common.Exceptions;
using Application.Common.Extensions;
using Application.Common.Interfaces;
using Application.TransactionCategories.Queries;

using AutoMapper;
using AutoMapper.QueryableExtensions;

using Domain.TransactionAggregate;
using Domain.TransactionCategoryAggregate;

using Microsoft.EntityFrameworkCore;

namespace Application.Transactions.Queries.GetCategory;

public record GetCategoryQuery : IQuery<CategoryDto> {
    [Required] public required int User { get; init; }
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

    public async Task<CategoryDto> Handle(GetCategoryQuery request, CancellationToken token = default) {
        var category = _context.TransactionCategories.AsNoTracking().Include(t => t.Profile)
            .FirstOrDefault(tc => tc.Id == request.Category
                                  && ((tc.ProfileId == request.Profile && tc.Profile!.UserId == request.User) || tc.ProfileId == null)
                                  && tc.DeletedAt == null);

        if (category is null) {
            _context.AssertUserOwnsProfile(request.User, request.Profile);
            throw new NotFoundValidationException(typeof(TransactionCategory));
        }

        return await Task.Run(() => _mapper.Map<CategoryDto>(category));
    }
}
