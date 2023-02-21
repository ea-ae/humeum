﻿using Application.Common.Exceptions;
using Application.Common.Interfaces;

using AutoMapper;
using AutoMapper.QueryableExtensions;

using Microsoft.EntityFrameworkCore;

namespace Application.Transactions.Queries.GetUserTransactions;

/// <summary>
/// Get transactions for a specified user with optional filtering conditions.
/// </summary>
public record GetUserTransactionsQuery : IQuery<List<TransactionDto>> {
    public required int User { get; init; }
    public required int Profile { get; init; }

    public DateOnly? StartBefore { get; init; }
    public DateOnly? StartAfter { get; init; }
}

public class GetUserTransactionsQueryHandler : IQueryHandler<GetUserTransactionsQuery, List<TransactionDto>> {
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public GetUserTransactionsQueryHandler(IAppDbContext context, IMapper mapper) {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<TransactionDto>> Handle(GetUserTransactionsQuery request, CancellationToken token) {
        var transactions = _context.Transactions.AsNoTracking()
                                                .Include(t => t.Profile)
                                                .Where(t => t.ProfileId == request.Profile && t.DeletedAt == null);

        bool userOwnsProfile = _context.Profiles.Any(p => p.Id == request.Profile && p.UserId == request.User);
        if (!userOwnsProfile) {
            throw new NotFoundValidationException("Profile ID not found.");
        }

        if (request.StartBefore is not null) {
            transactions = transactions.Where(t => t.PaymentTimeline.Period.Start < request.StartBefore);
        }
        if (request.StartAfter is not null) {
            transactions = transactions.Where(t => t.PaymentTimeline.Period.Start > request.StartAfter);
        }

        return await transactions.ProjectTo<TransactionDto>(_mapper.ConfigurationProvider).ToListAsync(token);
    }
}
